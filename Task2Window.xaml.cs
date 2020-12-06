﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KMiSOIB
{

    public partial class Task2Window : Window
    {
        public Task2Window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string message = MessageTB.Text;
            string key = KeyTB.Text;
            Task2 gost = new Task2(message, key);

            L0TB.Text = Utills.BinaryFormat(gost.l0, 4);
            R0TB.Text = Utills.BinaryFormat(gost.r0, 4);
            R0TB.Text = Utills.BinaryFormat(gost.r0, 4);
            X0TB.Text = Utills.BinaryFormat(gost.x0, 4);
            fR0X0TB.Text = Utills.BinaryFormat(gost.sumR0AndX0, 4);
            SubstitutionTB.Text = Utills.BinaryFormat(gost.filled, 4);
            ShiftTB.Text = Utills.BinaryFormat(gost.f, 4);
            R1TB.Text = Utills.BinaryFormat(gost.r1, 4);
        }
    }
}
