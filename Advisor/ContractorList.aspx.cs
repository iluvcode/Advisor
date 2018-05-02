using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Advisor.Entities;
using Data.Advisor;

namespace Advisor
{
    public partial class ContractorList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var catgeoryId = Request.QueryString["categoryId"];

            var contractorList = ContractorData.GetContractors(Convert.ToInt32(catgeoryId));

            ContractorListRepeater.DataSource = contractorList;
            ContractorListRepeater.DataBind();
        }

        protected void ContractorListRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
           
            var companyNameLabel = (Label) e.Item.FindControl("CompanyNameLabel");
            var addressLineLabel = (Label)e.Item.FindControl("AddressLineLabel");
            var cityLabel = (Label)e.Item.FindControl("CityLabel");
            var zipCodeLabel = (Label)e.Item.FindControl("ZipCodeLabel");

            var contractor = (Contractor) e.Item.DataItem;

            companyNameLabel.Text = contractor.CompanyName;
            addressLineLabel.Text = contractor.StreetAddress;
            cityLabel.Text = contractor.City;
            zipCodeLabel.Text = contractor.ZipCode;

        }

        protected void ContractorListRepeater_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}