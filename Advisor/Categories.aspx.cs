using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Data.Advisor;

namespace Advisor
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var categoriesList = CategoryData.GetCategories();

            var categoryDiv = new HtmlGenericControl("div");
            categoryDiv.Attributes.Add("class", "category");
            foreach (var category in categoriesList)
            {
               

                var labelDiv = new HtmlGenericControl("div");
                labelDiv.Attributes.Add("class","category-label");
                labelDiv.Controls.Add(new HyperLink
                {
                    NavigateUrl = category.Url,
                    ImageUrl = category.Image,
                    Text = category.ServiceType
                });

                categoryDiv.Controls.Add(labelDiv);


            }

            CategoriesPlaceholder.Controls.Add(categoryDiv);
        }
    }
}