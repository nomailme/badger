using Badger.Web.Models;
using Badger.Web.Processors.CertificateQuery;
using Badger.Web.Processors.CertificateQuery.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Badger.Web.Controllers
{
    public class CertificateQueryController : Controller
    {
        private readonly ILogger<CertificateQueryController> _logger;

        public CertificateQueryController(ILogger<CertificateQueryController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(Uri uri)
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
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
