using ColorToolPlusInternals;
using NUnit.Framework;

namespace ColorToolPlusTests
{
    [TestFixture]
    public class RgbTests
    {
        [Test]
        public void ColorTableValueCanBeConvertedToRgb()
        {
            const uint colorTableValue = 12228972;
            var rgb = colorTableValue.ToRgb();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(108, rgb.Red);
                Assert.AreEqual(153, rgb.Green);
                Assert.AreEqual(186, rgb.Blue);
            });
        }

        [Test]
        public void RgbCanBeConvertedToColorTableValue()
        {
            var rgb = new Rgb(172, 65, 66);
            var colorTableValue = rgb.ToColorTableValue();
            Assert.AreEqual(4342188, colorTableValue);
        }
    }
}
