using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Util;
using FFImageLoading;
using FFImageLoading.Helpers;
using FFImageLoading.Views;

namespace ImageTest {
    [Activity(Label = "ImageTest", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity {

        Dictionary<int, string> urlCache = new Dictionary<int, string> {
             {Resource.Id.image_1 , "http://www.drodd.com/images13/cat-drawing15.jpg"}
            ,{Resource.Id.image_2 , "http://www.drodd.com/images13/cat-drawing16.jpg"}
            ,{Resource.Id.image_3 , "http://www.drodd.com/images13/cat-drawing18.jpg"}
            ,{Resource.Id.image_4 , "http://www.drodd.com/images13/cat-drawing4.png"}
            ,{Resource.Id.image_5 ,  "http://www.drodd.com/images13/cat-drawing19.jpg"}
            ,{Resource.Id.image_6 ,  "http://www.drodd.com/images13/cat-drawing6.jpg"}
            ,{Resource.Id.image_7 ,  "http://www.drodd.com/images13/cat-drawing2.png"}
            ,{Resource.Id.image_8 ,  "http://www.drodd.com/images13/cat-drawing8.jpg"}
            ,{Resource.Id.image_9 ,  "http://expects_error.png"}
            ,{Resource.Id.image_10, "http://www.drodd.com/images13/cat-drawing10.gif"}
            ,{Resource.Id.image_11, "http://expects_error.png"}
            ,{Resource.Id.image_12, "http://www.drodd.com/images13/cat-drawing12.jpg"}
        };

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            ImageService.Instance.Initialize(new FFImageLoading.Config.Configuration {
                // VerboseLogging = true, // this does not exist in 2.1.1 
                Logger = new MyLogger()
            });

            foreach (var id in urlCache.Keys) {
                ImageService.Instance.LoadUrl(urlCache[id])
                            .DownSampleInDip(100, 100)
                            .ErrorPlaceholder("cat_error", FFImageLoading.Work.ImageSource.CompiledResource)
                            .LoadingPlaceholder("cat_loading", FFImageLoading.Work.ImageSource.CompiledResource)
                            .FadeAnimation(true, false, 500)
                            .Into(FindViewById<ImageViewAsync>(id));
            }
        }

        class MyLogger : IMiniLogger {
            public void Debug(string message) {
                Log.Debug("ImageLib", message);
            }

            public void Error(string errorMessage) {
                Log.Debug("ImageLib", errorMessage);
            }

            public void Error(string errorMessage, Exception ex) {
                Log.Debug("ImageLib", errorMessage, ex);
            }
        }
    }
}


