using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using GamePadKeyboard;
using GamePadKeyboard.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GamePad), typeof(GamePadRenderer))]

namespace GamePadKeyboard.Droid
{
    public class GamePadRenderer : ViewRenderer, TextureView.ISurfaceTextureListener
    {
        public GamePadRenderer(Context context) : base(context)
        {
        }

        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            throw new NotImplementedException();
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            throw new NotImplementedException();
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
            throw new NotImplementedException();
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
            throw new NotImplementedException();
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
        }
    }
}