using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Android.Util;
using SpiderEye.Bridge.Models;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SpiderEye.Android
{
	public class AndroidApplication
	{
		internal static AndroidFormsApplication Instance { get; private set; }
		public Window Window { get; private set; }
		public IContentProvider ContentProvider { get; private set; }
		public IUriWatcher UriWatcher { get; private set; }
		private FormsAppCompatActivity avtivity;
		private List<object> _handlers = new List<object>();
		public static AndroidApplication CreateDefault(FormsAppCompatActivity activity)
		{
			var result = new AndroidApplication(activity);
			return result;
		}
		internal AndroidApplication(FormsAppCompatActivity activity)
		{
			this.avtivity = activity;
		}
		public AndroidApplication UseEmbededResourceProvider(string root, Assembly container)
		{
			this.ContentProvider = new EmbeddedContentProvider(root, container);
			return this;
		}
		public AndroidApplication UseHandlers(params object[] handlers)
		{
			this._handlers = this._handlers ?? new List<object>();
			this._handlers.AddRange(handlers);
			return this;
		}
		public AndroidApplication UseUriWatcher(string devServerUrl)
		{
			this.UriWatcher = new AndroidUriWatcher(devServerUrl);
			return this;
		}
		public void Run(string url)
		{
			Instance = new AndroidFormsApplication(this.avtivity);
			Application.Register(Instance, OperatingSystem.Linux);
			this.Window = new Window();
			var window = this.Window;
			foreach(var handler in this._handlers)
			{
				window.Bridge.AddGlobalHandler(handler);
			}
			SetDevSettings(window);
			Application.UriWatcher = this.UriWatcher;// new AndroidUriWatcher("http://localhost:8080");
			Application.ContentProvider = this.ContentProvider;// new EmbeddedContentProvider("quasar\\dist\\spa", typeof(SpiderEye.Sample.WeatherForecast).Assembly);
			Application.Run(window, url);

		}
		public static void Run(FormsAppCompatActivity activity)
		{
			//Instance = new XamarinFormsApplication(activity);
			//Application.Register(new XamarinFormsApplication(activity), OperatingSystem.Linux);
			//var window = new Window();
			//window.Bridge.AddGlobalHandler(new Handler());
			//SetDevSettings(window);
			//Application.UriWatcher = new AndroidUriWatcher("http://localhost:8080");
			//Application.ContentProvider = new EmbeddedContentProvider("quasar\\dist\\spa", typeof(SpiderEye.Sample.WeatherForecast).Assembly);
			//Application.Run(window, "http://local/index.html");// "file://android_asset/local.html");
		}

		[Conditional("DEBUG")]
		private static void SetDevSettings(Window window)
		{
			window.EnableDevTools = true;

			// this is just to give some suggestions in case something isn't set up correctly for development
			window.PageLoaded += (s, e) =>
			{
				if (!e.Success)
				{
					string message = $"Page did not load! Did you start the Angular dev server?";
					if (Application.OS == SpiderEye.OperatingSystem.Windows)
					{
						message += $"{System.Environment.NewLine}On Windows 10 you also have to allow localhost. More info can be found in the SpiderEye readme.";
					}

					MessageBox.Show(window, message, "Page load failed", MessageBoxButtons.Ok);
				}
			};
		}

	}
}
