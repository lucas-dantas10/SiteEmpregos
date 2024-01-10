using EasyAutomationFramework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SiteEmpregos.Model;
using System.Data;

namespace SiteEmpregos.Driver
{
    public class WebScrapper : Web
    {
        public DataTable GetData(string link)
        {
            if (driver == null)
                StartBrowser();

            List<Item> items = new List<Item>();

            Navigate(link);

            var elements = GetValue(TypeElement.Xpath, "/html/body/form/div[3]/div/div[2]/article/div[3]")
                            .element.FindElements(By.ClassName("descricao"));

            foreach (var element in elements)
            {
                Item item = new Item();

                string location = element.FindElement(By.ClassName("nome-empresa")).Text;
                int index = location.IndexOf('-');
                string locationFormated = location.Substring(index + 1);

                item.Title = element.FindElement(By.TagName("h2")).Text;
                item.Location = locationFormated;
                item.Description = element.FindElement(By.ClassName("resumo-vaga")).Text;
                item.Company = element.FindElement(By.ClassName("nome-empresa")).Text;
                item.DatePublication = element.FindElement(By.ClassName("publicado")).Text;

                items.Add(item);
            }

            return Base.ConvertTo(items);
        }
    }
}
