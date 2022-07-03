using System;
using System.Linq;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using JournalAPI.Services;
using JournalAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JournalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JournalController : ControllerBase {
    
    private readonly MongoDBService _mongoDBService;
    public JournalController(MongoDBService mongoDBService) 
    {
        _mongoDBService = mongoDBService;
    }
        
    [HttpGet]
    public async Task<List<JournalEntry>> Get()
    {
        return await this._mongoDBService.GetJournalsAsync();
    }

    [HttpPost()]    
    public async Task<ActionResult<JournalEntry>> Post(JournalEntry jeModel)
    {
        //var jsonString = model;//.ToString();
        //JournalEntry je = JsonConvert.DeserializeObject<JournalEntry>(jsonString);


        await this._mongoDBService.AddJournalsEntryAsync(jeModel);
        return CreatedAtAction(nameof(Post), new { id = jeModel.Id }, jeModel);

        //return new JournalEntry();
    }


    // [HttpPut("{id}")]
    // public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId) {}

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(string id) {}

}