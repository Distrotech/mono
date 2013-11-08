﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApp5
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var clock = MyExtensions.GetClock().AsQbservable().Select(_ => _).AsObservable();

            var input = Observable.FromEventPattern<TextChangedEventArgs>(textBox1, "TextChanged").Select(evt => ((TextBox)evt.Sender).Text).Throttle(TimeSpan.FromSeconds(.5)).DistinctUntilChanged();

            var xs = from word in input.StartWith("")
                     from length in Task.Factory.StartNew(() => { Thread.Sleep(500); return word.Length; })
                     select length;

            var res = xs.CombineLatest(clock, (len, now) => now.ToString() + " - Word length = " + len);

            res.ObserveOnDispatcher().Subscribe(s =>
            {
                label1.Text = s.ToString();
            });
        }
    }

    public class MyExtensions
    {
        public static IObservable<DateTime> GetClock()
        {
            return Observable.Interval(TimeSpan.FromSeconds(1)).Select(_ => DateTime.Now);
        }
    }
}
