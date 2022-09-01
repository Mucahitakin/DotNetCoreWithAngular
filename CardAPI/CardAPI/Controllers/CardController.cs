using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : Controller
    {
        private readonly CardDbContext cardDbContext;

        public CardController(CardDbContext cardDbContext)
        {
            this.cardDbContext = cardDbContext;
        }
        //Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var card=await cardDbContext.Card.ToListAsync();
            return Ok(card);
        }

        //Get single Card
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute]Guid id)
        {
            var card = await cardDbContext.Card.FirstOrDefaultAsync(z=>z.Id==id);
            if (id!=null)
            {
                return Ok(card);
            }
            else
            {
                return NotFound("Not Found Card");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = new Guid();// zaten id otomatik artıyor ekstra girmemize gerek yok.
            await cardDbContext.Card.AddAsync(card);
            await cardDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), new {id=card.Id }, card);
        }

        //update a card
        [HttpPut] 
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingCard = await cardDbContext.Card.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                existingCard.CardHolderName = card.CardHolderName;
                existingCard.CardNumber = card.CardNumber;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                existingCard.CVC = card.CVC;
                await cardDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card not Found");
        }
        //Delete a card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletedCard([FromRoute] Guid id)
        {
            var existingCard = await cardDbContext.Card.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                cardDbContext.Remove(existingCard);
                await cardDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card not Found");
        }



    }
}
