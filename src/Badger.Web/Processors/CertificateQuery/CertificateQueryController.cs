using Badger.Web.Models;
using Badger.Web.Processors.CertificateQuery;
using Badger.Web.Processors.CertificateQuery.Extensions;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Badger.Web.Controllers
{
    public class CertificateQueryController : Controller
    {
        private readonly ILogger<CertificateQueryController> _logger;

        public CertificateQueryController(ILogger<CertificateQueryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(Uri uri)
        {
            var model = new CertificateModel(uri);
            return View(model);
        }

        public async System.Threading.Tasks.Task<IActionResult> GifAsync(Uri uri)
        {
            var badger = new ImageBadger();

            var certificate = await uri.GetCertificateAsync();
            var info = certificate.GetCertificateInfo();

            var text = new List<LabeleldValue>
            {
                new LabeleldValue{ Label = "URI", Value= uri.ToString()},
                new LabeleldValue{ Label = "CN", Value= info.Subject},
                new LabeleldValue{ Label = "Issuer", Value= info.Issuer},
                new LabeleldValue{ Label = "NotBefore", Value= info.NotBefore.Value.ToString("dd.MM.yyyy")},
                new LabeleldValue{ Label = "NotAfter", Value= info.NotAfter.Value.ToString("dd.MM.yyyy")},
            };
            var result = badger.Create(text);
            return File(result, "image/png");

            //using (var image = new MagickImage(new MagickColor("#56667A"), 200, 80))
            //{
            //    var magic = new Drawables()
            //    .FontPointSize(12)
            //    .Font("PT Sans")
            //    .FillColor(new MagickColor("white"))
            //    .TextAlignment(TextAlignment.Left)
            //    .Text(2, 12, "Issuer: Magick.NET")
            //    .Text(2, 24, "CN: Magick.NET")
            //    .Draw(image);


            //    return this.File(image.ToByteArray(MagickFormat.Gif), "image/gif");
            //}
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
