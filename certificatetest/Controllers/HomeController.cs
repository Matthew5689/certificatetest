using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace certificatetest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string certThumbprint = "67C8058CCD5A1CDDD921C9804A5454B3D1B8E0E6";

            using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                certStore.Open(OpenFlags.ReadOnly);

                X509Certificate2Collection certCollection = certStore.Certificates.Find(
                                            X509FindType.FindByThumbprint,
                                            // Replace below with your certificate's thumbprint
                                            certThumbprint,
                                            false);
                // Get the first cert with the thumbprint
                X509Certificate2 cert = certCollection.OfType<X509Certificate2>().FirstOrDefault();

                if (cert is null)
                    throw new Exception($"Certificate with thumbprint {certThumbprint} was not found");

                // Use certificate
                ViewBag.Message = cert.FriendlyName;

                // Consider to call Dispose() on the certificate after it's being used, avaliable in .NET 4.6 and later
            }


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}