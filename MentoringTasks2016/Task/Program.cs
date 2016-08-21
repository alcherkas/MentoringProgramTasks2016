// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SampleSupport;
using SampleQueries;
using System.IO;
using Task.Data;

// See the ReadMe.html for additional information
namespace SampleQueries
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            RegisterDependencies();

            List<SampleHarness> harnesses = new List<SampleHarness>();

            LinqSamples linqHarness = Container.GetInstance<ISamples>() as LinqSamples;

            harnesses.Add(linqHarness);

            Application.EnableVisualStyles();

            using (SampleForm form = new SampleForm("HomeWork - Mihail Romanov", harnesses))
            {
                form.ShowDialog();
            }
        }

        private static LightInject.IServiceContainer Container { get; } = new LightInject.ServiceContainer();


        private static void RegisterDependencies()
        {
            Container.Register<IDataSource, DataSource>();
            Container.Register<ISamples, LinqSamples>();
        }
    }
}