using UnityEngine;
using UnityEditor;

namespace GlobalHive.UI.ModernUI
{
    public class MUIEditor : MonoBehaviour
    {
        [MenuItem("GameObject/Global Hive/Modern UI/Setup/Scene Basic", false, 0)]
        static void SCSTP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Setup/Scene Setup"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Animated Icons/Hamburger to Exit", false, 0)]
        static void AIHTE()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Animated Icons/Hamburger to Exit"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Animated Icons/Heart Pop", false, 0)]
        static void AIHP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Animated Icons/Heart Pop"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Animated Icons/Lock", false, 0)]
        static void AIL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Animated Icons/Lock"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Animated Icons/Message Bubbles", false, 0)]
        static void AILMB()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Animated Icons/Message Bubbles"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Animated Icons/Sand Clock", false, 0)]
        static void AISC()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Animated Icons/Sand Clock"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Animated Icons/Switch", false, 0)]
        static void AIS()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Animated Icons/Switch"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Animated Icons/Yes to No", false, 0)]
        static void AIYTN()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Animated Icons/Yes to No"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic/Blue", false, 0)]
        static void BBBL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic/Blue"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic/Brown", false, 0)]
        static void BBBRW()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic/Brown"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic/Gray", false, 0)]
        static void BBGR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic/Green", false, 0)]
        static void BBGRE()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic/Green"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic/Night", false, 0)]
        static void BBNI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic/Night"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic/Orange", false, 0)]
        static void BBOR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic/Orange"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic/Pink", false, 0)]
        static void BBPIN()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic/Pink"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic/Purple", false, 0)]
        static void BBPURP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic/Purple"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic/Red", false, 0)]
        static void BBRED()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic/Red"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic/White", false, 0)]
        static void BBWHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Gradient/Blue", false, 0)]
        static void BGBL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Gradient/Blue"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Gradient/Brown", false, 0)]
        static void BGBRW()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Gradient/Brown"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Gradient/Gray", false, 0)]
        static void BGGR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Gradient/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Gradient/Green", false, 0)]
        static void BGGRE()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Gradient/Green"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Gradient/Night", false, 0)]
        static void BGNI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Gradient/Night"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Gradient/Orange", false, 0)]
        static void BGOR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Gradient/Orange"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Gradient/Pink", false, 0)]
        static void BGPIN()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Gradient/Pink"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Gradient/Purple", false, 0)]
        static void BGPURP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Gradient/Purple"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Gradient/Red", false, 0)]
        static void BGRED()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Gradient/Red"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Gradient/White", false, 0)]
        static void BGWHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Gradient/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline/Blue", false, 0)]
        static void BOBL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline/Blue"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline/Brown", false, 0)]
        static void BOBRW()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline/Brown"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline/Gray", false, 0)]
        static void BOGR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline/Green", false, 0)]
        static void BOGRE()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline/Green"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline/Night", false, 0)]
        static void BONI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline/Night"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline/Orange", false, 0)]
        static void BOOR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline/Orange"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline/Pink", false, 0)]
        static void BOPIN()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline/Pink"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline/Purple", false, 0)]
        static void BOPURP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline/Purple"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline/Red", false, 0)]
        static void BORED()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline/Red"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline/White", false, 0)]
        static void BOWHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline Gradient/Blue", false, 0)]
        static void BOGBL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline Gradient/Blue"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline Gradient/Brown", false, 0)]
        static void BOGBRW()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline Gradient/Brown"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline Gradient/Gray", false, 0)]
        static void BOGBGR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline Gradient/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline Gradient/Green", false, 0)]
        static void BOGGRE()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline Gradient/Green"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline Gradient/Night", false, 0)]
        static void BOGNI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline Gradient/Night"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline Gradient/Orange", false, 0)]
        static void BOGOR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline Gradient/Orange"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline Gradient/Pink", false, 0)]
        static void BOGPIN()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline Gradient/Pink"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline Gradient/Purple", false, 0)]
        static void BOGPURP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline Gradient/Purple"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline Gradient/Red", false, 0)]
        static void BOGRED()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline Gradient/Red"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline Gradient/White", false, 0)]
        static void BOGWHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline Gradient/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline With Image/Gray", false, 0)]
        static void BOWIGRA()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline With Image/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Basic Outline With Image/White", false, 0)]
        static void BOWIWHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Basic Outline With Image/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Box Outline With Image/Gray", false, 0)]
        static void BOXIGRA()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Box Outline With Image/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Box Outline With Image/White", false, 0)]
        static void BOXIWHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Box Outline With Image/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Box With Image/Gray", false, 0)]
        static void CIRGRA()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Box With Image/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Box With Image/White", false, 0)]
        static void CIRWHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Box With Image/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Circle Outline With Image/Gray", false, 0)]
        static void CIRCOGRA()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Circle Outline With Image/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Circle Outline With Image/White", false, 0)]
        static void CIRCOWHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Circle Outline With Image/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Circle With Image/Gray", false, 0)]
        static void CIRCGRA()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Circle With Image/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Circle With Image/White", false, 0)]
        static void CIRCWHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Circle With Image/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded/Blue", false, 0)]
        static void ROBL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded/Blue"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded/Brown", false, 0)]
        static void ROBRW()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded/Brown"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded/Gray", false, 0)]
        static void ROGR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded/Green", false, 0)]
        static void ROGRE()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded/Green"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded/Night", false, 0)]
        static void RONI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded/Night"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded/Orange", false, 0)]
        static void ROOR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded/Orange"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded/Pink", false, 0)]
        static void ROPIN()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded/Pink"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded/Purple", false, 0)]
        static void ROPURP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded/Purple"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded/Red", false, 0)]
        static void RORED()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded/Red"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded/White", false, 0)]
        static void ROWHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Gradient/Blue", false, 0)]
        static void RGBL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Gradient/Blue"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Gradient/Brown", false, 0)]
        static void RGBRW()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Gradient/Brown"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Gradient/Gray", false, 0)]
        static void RGGR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Gradient/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Gradient/Green", false, 0)]
        static void RGGRE()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Gradient/Green"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Gradient/Night", false, 0)]
        static void RGNI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Gradient/Night"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Gradient/Orange", false, 0)]
        static void RGOR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Gradient/Orange"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Gradient/Pink", false, 0)]
        static void RGPIN()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Gradient/Pink"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Gradient/Purple", false, 0)]
        static void RGPURP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Gradient/Purple"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Gradient/Red", false, 0)]
        static void RGRED()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Gradient/Red"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Gradient/White", false, 0)]
        static void RGWHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Gradient/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline/Blue", false, 0)]
        static void ROBLU()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline/Blue"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline/Brown", false, 0)]
        static void RORW()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline/Brown"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline/Gray", false, 0)]
        static void ROR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline/Green", false, 0)]
        static void RORE()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline/Green"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline/Night", false, 0)]
        static void RONIG()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline/Night"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline/Orange", false, 0)]
        static void ROORA()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline/Orange"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline/Pink", false, 0)]
        static void ROPINK()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline/Pink"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline/Purple", false, 0)]
        static void ROPURPL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline/Purple"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline/Red", false, 0)]
        static void ROREDD()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline/Red"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline/White", false, 0)]
        static void ROWHIT()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline Gradient/Blue", false, 0)]
        static void ROGBL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline Gradient/Blue"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline Gradient/Brown", false, 0)]
        static void ROGRW()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline Gradient/Brown"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline Gradient/Gray", false, 0)]
        static void ROGRAY()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline Gradient/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline Gradient/Green", false, 0)]
        static void ROGREE()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline Gradient/Green"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline Gradient/Night", false, 0)]
        static void ROGNI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline Gradient/Night"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline Gradient/Orange", false, 0)]
        static void ROGOR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline Gradient/Orange"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline Gradient/Pink", false, 0)]
        static void ROGPIN()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline Gradient/Pink"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline Gradient/Purple", false, 0)]
        static void ROGPURP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline Gradient/Purple"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline Gradient/Red", false, 0)]
        static void ROGRED()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline Gradient/Red"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline Gradient/White", false, 0)]
        static void ROGWHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline Gradient/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline With Image/Gray", false, 0)]
        static void ROWIGRA()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline Gradient/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded Outline With Image/White", false, 0)]
        static void ROWIWHIT()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded Outline Gradient/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded With Image/Gray", false, 0)]
        static void RWIGRAY()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded With Image/Gray"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Buttons/Rounded With Image/White", false, 0)]
        static void RWIWHIT()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Buttons/Rounded With Image/White"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Dropdowns/Multi Select", false, 0)]
        static void DRPMD()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Dropdowns/Multi Select Dropdown"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Dropdowns/Standard", false, 0)]
        static void DRPSD()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Dropdowns/Standard Dropdown"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Hamburger Menus/Standard", false, 0)]
        static void HAMLEFT()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Hamburger Menus/Standard"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Horizontal Selector/Standard", false, 0)]
        static void HRZSL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Horizontal Selector/Standard"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Input Fields/Fading Input Field (Left Aligned)", false, 0)]
        static void IFFIFL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Input Fields/Fading Input Field (Left Aligned)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Input Fields/Fading Input Field (Middle Aligned)", false, 0)]
        static void IFFIFM()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Input Fields/Fading Input Field (Middle Aligned)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Input Fields/Fading Input Field (Right Aligned)", false, 0)]
        static void IFFIFR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Input Fields/Fading Input Field (Right Aligned)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Input Fields/Standard Input Field (Left Aligned)", false, 0)]
        static void IFSIFL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Input Fields/Standard Input Field (Left Aligned)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Input Fields/Standard Input Field (Middle Aligned)", false, 0)]
        static void IFSIFM()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Input Fields/Standard Input Field (Middle Aligned)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Input Fields/Standard Input Field (Right Aligned)", false, 0)]
        static void IFSIFR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Input Fields/Standard Input Field (Right Aligned)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Modal Windows/Style 1/Auto-Resizing", false, 0)]
        static void MWSOAR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Modal Windows/Style 1/Auto-Resizing"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Modal Windows/Style 1/Standard", false, 0)]
        static void MWSOS()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Modal Windows/Style 1/Standard"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Modal Windows/Style 1/With Buttons", false, 0)]
        static void MWSOWB()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Modal Windows/Style 1/With Buttons"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Modal Windows/Style 1/With Tabs", false, 0)]
        static void MWSOWT()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Modal Windows/Style 1/With Tabs"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone; Debug.Log("No object found named 'Canvas', creating the object separately.");
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Modal Windows/Style 2/Standard", false, 0)]
        static void MWSTS()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Modal Windows/Style 2/Standard"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Modal Windows/Style 2/With Tabs", false, 0)]
        static void MWSTWT()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Modal Windows/Style 2/With Tabs"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Notifications/Notification", false, 0)]
        static void NFN()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Notifications/Notification"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Notifications/Icon Popup Bottom Left", false, 0)]
        static void NIPBL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Notifications/Fading Icon Popup Bottom Left"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Notifications/Icon Popup Bottom Right", false, 0)]
        static void NIPBR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Notifications/Icon Popup Bottom Right"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Notifications/Icon Popup Top Left", false, 0)]
        static void NIPTL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Notifications/Icon Popup Top Left"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Notifications/Icon Popup Top Right", false, 0)]
        static void NIPTR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Notifications/Icon Popup Top Right"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }


        [MenuItem("GameObject/Global Hive/Modern UI/Progress Bars/Radial PB Bold", false, 0)]
        static void PBRPB()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Progress Bars/Radial PB Bold"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Progress Bars/Radial PB Filled H", false, 0)]
        static void PBRPF()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Progress Bars/Radial PB Filled H"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Progress Bars/Radial PB Filled V", false, 0)]
        static void PBRPV()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Progress Bars/Radial PB Filled V"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Progress Bars/Radial PB Light", false, 0)]
        static void PBRPLI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Progress Bars/Radial PB Light"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Progress Bars/Radial PB Regular", false, 0)]
        static void PBRPREG()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Progress Bars/Radial PB Regular"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Progress Bars/Radial PB Thin", false, 0)]
        static void PBRPTHI()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Progress Bars/Radial PB Thin"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Progress Bars/Standard PB", false, 0)]
        static void PBSPB()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Progress Bars/Standard PB"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }     

        [MenuItem("GameObject/Global Hive/Modern UI/Progress Bars (Loop)/Circle Glass", false, 0)]
        static void PBLCG()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Circle Glass"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Progress Bars (Loop)/Circle Pie", false, 0)]
        static void PBLPIE()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Circle Pie"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Progress Bars (Loop)/Circle Run", false, 0)]
        static void PBLRUN()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Circle Run"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Progress Bars (Loop)/Standard Fastly", false, 0)]
        static void PBLSF()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Standard Fastly"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Progress Bars (Loop)/Standard Run", false, 0)]
        static void PBLSRUN()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Progress Bars (Loop)/Standard Run"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Gradient", false, 0)]
        static void SLGR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Gradient"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Gradient (Popup)", false, 0)]
        static void SLGRP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Gradient (Popup)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Gradient (Value)", false, 0)]
        static void SLGRV()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Gradient (Value)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Outline", false, 0)]
        static void SLO()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Outline"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Outline (Popup)", false, 0)]
        static void SLOP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Outline (Popup)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Outline (Value)", false, 0)]
        static void SLOV()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Outline (Value)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Radial Gradient", false, 0)]
        static void SLRG()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Radial Gradient"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Radial Standard", false, 0)]
        static void SLRS()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Radial Standard"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Range", false, 0)]
        static void SLRAN()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Range"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Range (Clean)", false, 0)]
        static void SLRANC()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Range (Clean)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Range (Label)", false, 0)]
        static void SLRANL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Range (Label)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Standard", false, 0)]
        static void SLSTA()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Standard"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Standard (Popup)", false, 0)]
        static void SLSTAP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Standard (Popup)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Sliders/Standard (Value)", false, 0)]
        static void SLSTAV()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Sliders/Standard (Value)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Switches/Material", false, 0)]
        static void SWMAT()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Switches/Material"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Switches/Standard", false, 0)]
        static void SWST()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Switches/Standard"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Switches/Standard With Label", false, 0)]
        static void SWSTL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Switches/Standard With Label"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Toggles/Standard Toggle (Bold)", false, 0)]
        static void TSTB()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Toggles/Standard Toggle (Bold)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Toggles/Standard Toggle (Light)", false, 0)]
        static void TSTL()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Toggles/Standard Toggle (Light)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone; Debug.Log("No object found named 'Canvas', creating the object separately.");
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Toggles/Standard Toggle (Regular)", false, 0)]
        static void TSTR()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Toggles/Standard Toggle (Regular)"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Toggles/Toggle Group Panel", false, 0)]
        static void TSTTG()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Toggles/Toggle Group Panel"), Selection.activeTransform) as GameObject;
            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Tool Tips/Fading Tool Tip", false, 0)]
        static void TPFTP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Tool Tips/Fading Tool Tip"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Tool Tips/Scaling Tool Tip", false, 0)]
        static void TPSTP()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Tool Tips/Scaling Tool Tip"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        // Global Hive

        [MenuItem("GameObject/Global Hive/Modern UI/Panels/Basic", false, 0)]
        static void PNLB()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Panels/Panel"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/TreeView/Tree View", false, 0)]
        static void TV()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("TreeView/TreeView"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }

        [MenuItem("GameObject/Global Hive/Modern UI/Tabs/Tabs", false, 0)]
        static void STTAB()
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Tabs/Tabs"), Selection.activeTransform) as GameObject;

            clone.name = clone.name.Replace("(Clone)", "").Trim();
            Selection.activeGameObject = clone;
        }
    }
}