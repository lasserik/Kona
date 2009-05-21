using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Kona.Infrastructure;

namespace Kona.Data {
    [DataContract()]
    public class KonaSite : Site {
        IObjectStore _store;
        public KonaSite() {
            IObjectStore store = new ObjectStore();
        }
        public KonaSite(IObjectStore store) {
            _store = store;
        }
        [DataMember]
        public bool AcceptPayPal { get; set; }
        [DataMember]
        public string PayPalBusinessEmail { get; set; }
        [DataMember]
        public string PayPalPDTToken { get; set; }

        [DataMember]
        public bool AcceptCreditCards { get; set; }
        [DataMember]
        public bool AcceptPOs { get; set; }
        [DataMember]
        public string CreditCardProcessor { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }


        [DataMember]
        public string AddressForm { get; set; }
        [DataMember]
        public string CreditCardForm { get; set; }
        [DataMember]
        public string PayPalForm { get; set; }
        [DataMember]
        public string OrderHeaderView { get; set; }
        [DataMember]
        public string OrderDetailView { get; set; }

        [DataMember]
        public string TaxPlugin { get; set; }
        [DataMember]
        public string ShippingPlugin { get; set; }

        public static KonaSite GetSite(string url) {
            IObjectStore store=new ObjectStore();
            KonaSite site = store.Get<KonaSite>("Site", url);
            if (site == null)
            {
                site = new KonaSite();

                site.AdminEmail = "rob@wekeroad.com";
                site.Description = "Fuji Test Site";
                site.IsPublic = true;
                site.LanguageCode = "en";
                site.Owner = "admin";
                site.OwnerName = "Microsoft Corporation";
                site.SiteHost = "localhost";
                site.SiteName = "Kona Demo Site";
                site.SiteUrl = url;
                site.SiteID = Guid.NewGuid();
                site.RPXAPIKey = "9b9002f34658471c99ac569e125dca9afa095132";
                site.RPXNowUrl = "https://localhost18622.rpxnow.com/openid/embed";
                site.AddressForm = "USDefault";
                site.AcceptCreditCards = true;
                site.AcceptPayPal = true;
                site.AcceptPOs = false;
                site.PayPalPDTToken = "JijaVlgNlwzXc5N_Zj53LS-v5EmzqsQGMa6eZcKyXad8hH7dn08ntEZlcAW";
                site.CreditCardForm = "DefaultCreditCard";
                site.PayPalForm = "DefaultPayPalStandard";
                site.CurrencyCode = "USD";
                site.PayPalBusinessEmail = "seller_1223063242_biz@hotmail.com";
                site.OrderDetailView = "DefaultItems";
                site.OrderHeaderView = "DefaultHeader";

                site.SMTPLogin = "MY Secured Login :)";
                site.SMTPPassword = "Can't See Me";
                site.SMTPServer = "Myserver.com";
                site.TaxPlugin = "USTaxCalculator";
                site.ShippingPlugin = "SimpleShippingCalculator";
                site.CreditCardProcessor = "FakeProcessor";
            }

            // In case the deserialization format changed or sth went wrong
            if (string.IsNullOrEmpty(site.ThemeName)) {
                site.ThemeName = "Eko";
            }

            return site;
        }

        public void Save() {
            _store.Store<KonaSite>("Site", this.SiteUrl, this);
        }

        public void SavePluginSettings(string id, PluginSetting setting) {
            _store.Store<PluginSetting>("PluginSetting", id, setting);
        }

        public void LoadSettings(List<Plugin> plugs) {
            plugs.ForEach(x => x.Settings = _store.Get<PluginSetting>("PluginSetting", x.GetType().Name) ?? new PluginSetting());
        }
    
    
    }
}
