using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lb
{
    class clsUtils
    {
        public static int GetNumberOfWorkingDays(DateTime start, DateTime stop)
        {
            TimeSpan interval = stop - start;

            int totalWeek = interval.Days / 7;
            int totalWorkingDays = 5 * totalWeek;

            int remainingDays = interval.Days % 7;


            for (int i = 0; i <= remainingDays; i++)
            {
                DayOfWeek test = (DayOfWeek)(((int)start.DayOfWeek + i) % 7);
                if (test >= DayOfWeek.Monday && test <= DayOfWeek.Friday)
                    totalWorkingDays++;
            }

            return totalWorkingDays;
        }
        public static  double WorkDays(DateTime startDate, DateTime endDate)
        {
            double weekendDays;

            double days = endDate.Subtract(startDate).TotalDays;

            if (days < 0) return 0;

            DateTime startMonday = startDate.AddDays(DayOfWeek.Monday - startDate.DayOfWeek).Date;
            DateTime endMonday = endDate.AddDays(DayOfWeek.Monday - endDate.DayOfWeek).Date;

            weekendDays = ((endMonday.Subtract(startMonday).TotalDays) / 7) * 2;

            // compute fractionary part of weekend days
            double diffStart = startDate.Subtract(startMonday).TotalDays - 5;
            double diffEnd = endDate.Subtract(endMonday).TotalDays - 5;

            // compensate weekenddays
            if (diffStart > 0) weekendDays -= diffStart;
            if (diffEnd > 0) weekendDays += diffEnd;

            return days - weekendDays;
        }
        public static (Int32,string) get_score_between_2_strings(string p_string01, string p_string02)
        {
            string m_string01_replaced = "";
            string m_string02_replaced = "";
            string m_string01_no_space = "";
            string m_string02_no_space = "";
            //string m_string01_and_string02 = "";

            Int32 m_score = 0;
            Int32 m_score01 = 0;
            Int32 m_score02 = 0;
            string m_string_for_score = "";
            try
            {
                m_string_for_score = p_string01;
                //*
                //* If EXACT, then score is 100
                //*
                if (p_string01.ToLower() == p_string02.ToLower())
                {
                    m_score = 100;
                    return (m_score, m_string_for_score);
                }
                //*
                //* Now, ADD SPACE bofore the string and break it into words
                //*
                m_string01_replaced = " " + p_string01.ToLower();
                m_string02_replaced = " " + p_string02.ToLower();
                m_string01_no_space = p_string01.Replace(" ", "").ToLower();
                m_string02_no_space = p_string02.Replace(" ", "").ToLower();
                //*
                //* Split m_string01 in to ARRAY and remove every word from m_string02_replaced
                //*

                string[] strings_words01 = p_string01.ToLower().Split(' ');
                foreach (string string_words01 in strings_words01)
                {
                    m_string02_replaced = m_string02_replaced.Replace(" "+string_words01, "");
                }
                m_string02_replaced = m_string02_replaced.Replace(" ", "");
                //*
                //* Split m_string02 in to ARRAY and remove every word from m_string01_replaced
                //*
                string[] strings_words02 = p_string02.ToLower().Split(' ');
                foreach (string string_words02 in strings_words02)
                {
                    m_string01_replaced = m_string01_replaced.Replace(" "+string_words02, "");
                }
                m_string01_replaced = m_string01_replaced.Replace(" ", "");
                if (m_string01_no_space.Length == m_string01_replaced.Length)
                {
                    m_score01 = 0;
                }
                else
                {
                    m_score01 = (( m_string01_no_space.Length * 100 - m_string01_replaced.Length * 100) / m_string01_no_space.Length)/2;
                }
                if (m_string02_no_space.Length == m_string02_replaced.Length)
                {
                    m_score02 = 0;
                }
                else
                {
                    m_score02 = ((m_string02_no_space.Length * 100 - m_string02_replaced.Length * 100) / m_string02_no_space.Length)/2;
                }
                m_score = m_score01 + m_score02;
                if(m_score>0)
                {

                }
                return (m_score, m_string_for_score);

            }
            catch (Exception ex)
            {
                return (m_score, m_string_for_score);
            }
        }

        //public static string Soundex(string word)
        //{
        //    //word = word.ToUpper();
        //    //word = word[0] +
        //    //    Regex.Replace(
        //    //        Regex.Replace(
        //    //        Regex.Replace(
        //    //        Regex.Replace(
        //    //        Regex.Replace(
        //    //        Regex.Replace(
        //    //        Regex.Replace(word.Substring(1), "[AEIOUYHW]", ""),
        //    //        "[BFPV]+", "1"),
        //    //        "[CGJKQSXZ]+", "2"),
        //    //        "[DT]+", "3"),
        //    //        "[L]+", "4"),
        //    //        "[MN]+", "5"),
        //    //        "[R]+", "6")
        //    //    ;
        //    //return word.PadRight(4, '0').Substring(0, 4);
        //}


    }
}
