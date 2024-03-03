using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ValetaxProj.Models;
using ValetaxProj.Services.Interfaces;

namespace ValetaxProj.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NodesController : Controller
    {
        private readonly INodeService _nodeService;

        public NodesController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        [HttpGet(Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _nodeService.GetNode(id));
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _nodeService.GetAllNodes());
        }

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Create(NodeViewModel model)
        {
            await _nodeService.AddNode(model);

            return Ok(model);
        }

        [HttpPut(Name = "Edit")]
        public async Task<IActionResult> Edit(NodeViewModel node)
        {
            await _nodeService.UpdateNode(node);
            return Ok(node);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _nodeService.DeleteNode(id);
            return Ok();
        }

    }
}
