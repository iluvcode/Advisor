using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data.Advisor;

namespace Advisor
{
    public partial class ContractorInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var contractorId = Request.QueryString["ContractorId"];

            var contractorDetails = ContractorData.GetContractor(Convert.ToInt32(contractorId));

            NameLabel.Text = contractorDetails.CompanyName;
            AddressLineLabel.Text = contractorDetails.StreetAddress;
            CityLabel.Text = contractorDetails.City;
            ZipCodeLabel.Text = ZipCodeLabel.Text;
        }
    }
}