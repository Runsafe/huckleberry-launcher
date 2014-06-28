using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuckleberryLauncher
{
    class SplashPlayerImage : ClickThroughPictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (this.Image != null)
            {
                pe.Graphics.TranslateTransform(this.Width - this.Image.Width, this.Height - this.Image.Height);
            }
            base.OnPaint(pe);
        }
    }
}
