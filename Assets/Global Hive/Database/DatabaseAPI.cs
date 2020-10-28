using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GlobalHive.DatabaseAPI
{
    public class API
    {
        #region Settings
        public static readonly string HOST = "94.231.94.116";
        public static readonly int PORT = 3306;
        public static readonly string DATABASE = "CL13148_Korallen";
        public static readonly string USERNAME = "DKorallenB";
        public static readonly string PASSWORD = "Xlpa956*";
        public static readonly string SSL_MODE = "none";

        // Die Anzahl an Verbindungen, die gleichzeitig offen sind.
        public static readonly int POOL_SIZE = 8;

        // Der Intervall (in Minuten) in dem die Verbindungen erneuert werden.
        public static readonly int RECONNECT_INTERVAL = 180;
        #endregion

        private static API instance;

        private List<MySqlConnection> availableConnections;

        private API()
        {
            this.availableConnections = new List<MySqlConnection>();
            Logger.Instance.Log("Starting Database");
            PopulatePool();
        }

        /// <summary>
        /// Gibt die Singleton-Instanz der DatabaseAPI zurück.
        /// </summary>
        /// <returns>Die Singleton-Instanz der DatabaseAPI.</returns>
        public static API GetInstance()
        {
            if (instance == null)
            {
                instance = new API();
            }

            return instance;
        }


        public static void executeNonQuery(MySqlCommand cmd)
        {
            MySqlConnection conn = GetInstance().GetConnection();
            cmd.Connection = conn;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Logger.Instance.LogE("DB ERROR: " + ex.ErrorCode + "|" + ex.Message);
                //Debug.WriteLine("DB ERROR: " + ex.ErrorCode + "|" + ex.Message);
                throw ex;
            }
            finally
            {
                GetInstance().FreeConnection(conn);
            }
        }


        /// <summary>
        /// Gibt eine Verbindung aus dem Pool zurück.
        /// </summary>
        /// <returns>Eine Verbindung aus dem Pool.</returns>
        public MySqlConnection GetConnection()
        {
            MySqlConnection connection = null;

            do
            {
                Monitor.Enter(availableConnections);

                bool sleep = false;

                if (availableConnections.Count > 0)
                {
                    connection = availableConnections[0];

                    availableConnections.RemoveAt(0);
                }
                else
                {
                    sleep = true;
                }

                Monitor.Exit(availableConnections);

                if (sleep)
                {
                    Task.Delay(1).Wait();
                }

            } while (connection == null);

            connection.Open();
            
            return connection;
        }

        /// <summary>
        /// Übergibt eine Verbindung zurück an den Pool.
        /// </summary>
        /// <param name="connection">Die Verbindung, die an den Pool zurückgegeben wird.</param>
        public void FreeConnection(MySqlConnection connection)
        {

            //TODO: Entfernen. Ist nur zum testen
            if (IsReaderAttached(connection))
            {
                string errorMessage = "[DatabaseAPI] Datenbank-Reader nicht geschlossen! Letzte Stack-Frames:\n";

                StackFrame[] frames = new StackTrace().GetFrames();
                int interestingFrameCount = Math.Min(frames.Length, 10);

                for (int i = 0; i < interestingFrameCount; i++)
                {
                    StackFrame frame = frames[i];

                    errorMessage += ("Index " + i).PadRight(15) + "Class=" + frame.GetMethod().DeclaringType.FullName.PadRight(40) + "Method=" + frame.GetMethod().Name + "\n";
                }

                Logger.Instance.LogE(errorMessage);
                Console.WriteLine(errorMessage);
            }

            Monitor.Enter(availableConnections);
            connection.Close();
            availableConnections.Add(connection);

            Monitor.Exit(availableConnections);
        }

        /// <summary>
        /// Füllt den Pool mit Verbindungen.
        /// </summary>
        private void PopulatePool()
        {
            string connectionDescription = "Server=" + HOST + ";Database=" + DATABASE + ";Port=" + PORT + ";User Id=" + USERNAME + ";Password=" + PASSWORD + ";SSLMODE=" + SSL_MODE;

            for (int i = 0; i < POOL_SIZE; i++)
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(connectionDescription);
                    //connection.Open();

                    availableConnections.Add(connection);

                }
                catch (Exception exc)
                {
                    Logger.Instance.LogE("[DatabaseAPI] Beim Erstellen der Pool-Population ist folgender Fehler aufgetreten: " + exc.Message);
                    //Console.WriteLine("[DatabaseAPI] Beim Erstellen der Pool-Population ist folgender Fehler aufgetreten: " + exc.Message);
                }
            }
        }

        private bool IsReaderAttached(MySqlConnection conn)
        {
            try
            {
                string sql = "SELECT 0;";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    rdr.Close();
                }
            }
            catch (Exception e)
            {
                return true;
            }

            return false;
        }
    }
}

