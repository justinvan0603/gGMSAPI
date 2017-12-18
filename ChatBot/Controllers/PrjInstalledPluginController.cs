 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatBot.Models;
using ChatBot.ViewModels;
using Microsoft.EntityFrameworkCore;
using ChatBot.Infrastructure.Core;
using System.Xml.Linq;
using System.Threading;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/PrjInstalledPlugin")]
    public class PrjInstalledPluginController : Controller
    {
        private readonly gGMSContext _context;
        public PrjInstalledPluginController(gGMSContext context)
        {
            this._context = context;
        }
        [HttpGet("{pluginid}")]
        public async Task<IEnumerable<PrjInstalledPluginViewModel>> Get(string pluginid)
        {
            string pluginCommand = $"dbo.PRD_PLUGIN_Search @p_PLUGIN_ID = '{pluginid}'";
            var pluginResult = await _context.PrdPlugin.FromSql(pluginCommand).ToListAsync();
            var selectedPlugin = pluginResult.Single(plg => plg.PluginId.Equals(pluginid));
            string prjInstalledPluginCommand = $"dbo.PRJ_INSTALLED_PLUGIN_Search @p_PLUGIN_ID = '{pluginid}', @p_TOP = ''";
            var listPrjInstalledPluginResult = await _context.PrjInstalledPlugins.FromSql(prjInstalledPluginCommand).ToListAsync();
            string listProjectCommand = $"dbo.PRJ_PROJECT_MASTER_Search @p_TOP = ''";
            var listProjectResult = await _context.PrjProjectMaster.FromSql(listProjectCommand).ToListAsync();
            
            List<PrjInstalledPluginViewModel> listPrjInstalledPluginViewModel = new List<PrjInstalledPluginViewModel>();
            foreach (var item in listPrjInstalledPluginResult)
            {
                PrjInstalledPluginViewModel newObj = new PrjInstalledPluginViewModel();
                newObj.PrjProjectMaster = listProjectResult.Single(prj => prj.PROJECT_ID.Equals(item.PROJECT_ID));
                newObj.PrdPlugin = selectedPlugin;
                newObj.PrjInstalledPlugin = item;
                newObj.IsChecked = true;
                listPrjInstalledPluginViewModel.Add(newObj);
            }
            listProjectResult = listProjectResult.Where(prj => !listPrjInstalledPluginResult.Any(ip => ip.PROJECT_ID.Equals(prj.PROJECT_ID))).ToList();
            foreach (var item in listProjectResult)
            {
                PrjInstalledPluginViewModel newObj = new PrjInstalledPluginViewModel();
                newObj.PrdPlugin = selectedPlugin;
                newObj.PrjInstalledPlugin = new PrjInstalledPlugin();
                newObj.PrjProjectMaster = item;
                newObj.PrjInstalledPlugin.PLUGIN_ID = pluginid;
                newObj.PrjInstalledPlugin.IS_CHECKED = "0";
                newObj.IsChecked = false;
                newObj.PrjInstalledPlugin.PROJECT_ID = item.PROJECT_ID;
                newObj.PrjInstalledPlugin.PROJECT_NAME = item.PROJECT_NAME;
                newObj.PrjInstalledPlugin.SUBDOMAIN = item.SUB_DOMAIN;
                listPrjInstalledPluginViewModel.Add(newObj);
            }
            return listPrjInstalledPluginViewModel;
        }
        [HttpPost]
        public async Task<ObjectResult> Post([FromBody] List<PrjInstalledPluginViewModel> listPrjInstalledPlugin)
        {
            GenericResult rs = new GenericResult();
            try
            {
                XElement xmlInstalledPlugins = new XElement(new XElement("Root"));
                XAttribute encodingAttribute = new XAttribute("encoding", "utf-8");
                xmlInstalledPlugins.Add(encodingAttribute);   
                var listAdding = listPrjInstalledPlugin.Where(plg => plg.IsChecked == true).ToList();
                foreach (var item in listAdding)
                {
                    XElement childElement = new XElement("PrjInstalledPlugin", new XElement("PLUGIN_ID", item.PrdPlugin.PluginId),
                                                     new XElement("PROJECT_ID", item.PrjInstalledPlugin.PROJECT_ID),
                                                     new XElement("PROJECT_NAME", item.PrjInstalledPlugin.PROJECT_NAME),
                                                     new XElement("IS_CHECKED", "1"),
                                                     new XElement("SUBDOMAIN", item.PrjInstalledPlugin.SUBDOMAIN));
                    xmlInstalledPlugins.Add(childElement);
                }
                string command = $"dbo.PRJ_INSTALLED_PLUGIN_Ins @p_PLUGIN_ID = '{listPrjInstalledPlugin[0].PrdPlugin.PluginId}',@p_INSTALLED_PLUGIN = '{xmlInstalledPlugins}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result != -1)
                {
                    rs.Succeeded = true;
                    rs.Message = "Danh sách cập nhật plugin đã được cập nhật!";
                }
                else
                {
                    rs.Succeeded = false;
                    rs.Message = "Xảy ra lỗi vui lòng thử lại sau";
                }
                ObjectResult objRes = new ObjectResult(rs);
                return objRes;
            }
            catch (Exception ex)
            {
                rs.Succeeded = false;
                rs.Message = "Lỗi - " + ex.Message;
                ObjectResult objRes = new ObjectResult(rs);
                return objRes;
            }
        }
    } 
}