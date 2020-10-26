using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DateTimePicker : Singleton<DateTimePicker>
{
    [Header("REFERENCES")]
    [SerializeField] TMP_Text Year;
    [SerializeField] TMP_Text Month;
    [SerializeField] Transform NumbersParent;
    [SerializeField] Animator Animator;
    [SerializeField] Transform MonthsParent;
    [SerializeField] Transform YearsParent;

    [Header("DATE")]
    [SerializeField] int SelectedYear;
    [SerializeField] int SelectedMonth;
    [SerializeField] int SelectedDay;

    [Header("TIME")]
    [SerializeField] int SelectedHour;
    [SerializeField] int SelectedMinute;

    [Header("SETTINGS")]
    [SerializeField] Color ActiveColor;
    [SerializeField] Color InactiveColor;
    [SerializeField] Color SelectedActiveColor;
    [SerializeField] Color SelectedInactiveColor;
    [SerializeField] Color TextNormalColor;
    [SerializeField] Color TextHoverColor;

    int _DaysInSelectedMonth;
    DateTime _FirstDayOfMonth;

    int _DaysInLastMonth;

    bool isVisible = false;

    void SetDateTime()
    {
        int _StartID;
        int _CurrentID = 1;
        int _TempYear;

        if (SelectedYear == 0)
            SelectedYear = DateTime.Now.Year;

        if (SelectedMonth == 0)
            SelectedMonth = DateTime.Now.Month;

        if (SelectedDay == 0)
            SelectedDay = DateTime.Now.Day;

        _DaysInSelectedMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);
        _FirstDayOfMonth = new DateTime(SelectedYear, SelectedMonth, 1);

        Year.SetText(SelectedYear.ToString());
        Month.SetText(SelectedDay + ". " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(SelectedMonth));
        MonthsParent.GetChild(SelectedMonth-1).GetComponent<Toggle>().isOn = true;

        _StartID = (int)_FirstDayOfMonth.DayOfWeek-1;

        if (_StartID == -1)
            _StartID = 6;

        if (SelectedMonth - 1 == 0)
            _DaysInLastMonth = DateTime.DaysInMonth(SelectedYear - 1, 12);
        else
            _DaysInLastMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth - 1);
        int _BackDays = _StartID-1;


        for (int i = 0; i < _StartID; i++)
        {
            TMP_Text _TempText = NumbersParent.GetChild(i).GetComponentInChildren<TMP_Text>();
            NumbersParent.GetChild(i).GetComponent<Image>().color = SelectedInactiveColor;
            _TempText.SetText((_DaysInLastMonth-_BackDays).ToString());
            _TempText.color = InactiveColor;
            _BackDays--;
        }

        for (int i = _StartID; i < _DaysInSelectedMonth+_StartID; i++)
        {
            TMP_Text _TempText = NumbersParent.GetChild(i).GetComponentInChildren<TMP_Text>();
            _TempText.SetText(_CurrentID.ToString());
            _TempText.color = ActiveColor;

            Button _TempButton = NumbersParent.GetChild(i).GetComponent<Button>();
            _TempButton.name = _CurrentID.ToString();
            _TempButton.onClick.AddListener(delegate { Click(_TempButton); });

            if (_CurrentID == DateTime.Today.Day)
                _TempButton.GetComponent<Image>().color = SelectedActiveColor;
            else
                _TempButton.GetComponent<Image>().color = SelectedInactiveColor;

            _CurrentID++;
        }

        _CurrentID = 1;

        for (int i = _DaysInSelectedMonth+_StartID; i < NumbersParent.childCount; i++)
        {
            TMP_Text _TempText = NumbersParent.GetChild(i).GetComponentInChildren<TMP_Text>();
            NumbersParent.GetChild(i).GetComponent<Image>().color = SelectedInactiveColor;
            _TempText.SetText(_CurrentID.ToString());
            _TempText.color = InactiveColor;
            _CurrentID++;
        }
    }

    public void PointerUp(bool _IsYear)
    {
        if (_IsYear)
        {
            //Animator.Play("SelectYear-In");
        }
        else
        {
            Animator.Play("SelectMonth-In");
        }
    }

    public void PointerEnter(TMP_Text _Text)
    {
        _Text.color = TextHoverColor;
    }

    public void PointerExit(TMP_Text _Text)
    {
        _Text.color = TextNormalColor;
    }

    public void PointerUpMonth(int _Month)
    {
        SelectedMonth = _Month;
        SetDateTime();
        Animator.Play("SelectMonth-Out");
    }

    public void PointerUpYear(int _Year)
    {
        SelectedYear = _Year;
        SetDateTime();
        Animator.Play("SelectYear-Out");
    }

    void Click(Button _Button)
    {
        if (_Button.name == SelectedDay.ToString())
            return;

        foreach (Transform item in NumbersParent)
        {
            Button _TempButton = item.GetComponent<Button>();
            if(_TempButton == _Button)
                _TempButton.GetComponent<Image>().color = SelectedActiveColor;
            else
                _TempButton.GetComponent<Image>().color = SelectedInactiveColor;
        }
        SelectedDay = int.Parse(_Button.name);

        Month.SetText(SelectedDay + ". " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(SelectedMonth));
    }

    public void Switch()
    {
        if (isVisible)
            Animator.Play("Fade-Out");
        else
            Animator.Play("Fade-In");

        isVisible = !isVisible;
    }

    public DateTime GetSelectedDate()
    {
        return new DateTime(SelectedYear, SelectedMonth, SelectedDay);
    }

    public DateTime GetSelectedDateTime()
    {
        return new DateTime(SelectedYear, SelectedMonth, SelectedDay, SelectedHour, SelectedMinute, 0);
    }
}
