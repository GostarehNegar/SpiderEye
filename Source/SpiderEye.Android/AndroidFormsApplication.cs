using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;

namespace SpiderEye.Android
{
	class AndroidFormsApplication : IApplication
	{
		public FormsAppCompatActivity MainActivity { get; private set; }
		public AndroidFormsApplication(FormsAppCompatActivity mainActivity)
		{
			this.MainActivity = mainActivity;
			this.SynchronizationContext = SynchronizationContext.Current;
		}
		public IUiFactory Factory => new AndroidUiFactory(this.MainActivity);

		public SynchronizationContext SynchronizationContext { get; private set; }

		public void Exit()
		{
			//throw new NotImplementedException();
		}

		public void Run()
		{
			//throw new NotImplementedException();
		}
	}
}