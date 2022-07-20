using Microsoft.AspNetCore.Mvc;
using Application.Services.Position;
using API.Helpers;
using API.Models;

namespace API.Controllers
{
    public class PositionController : Controller
    {
        [HttpPost]
        public async Task<int> CreateNewPosition(
            PositionModel positionModel,
            CancellationToken cancellationToken)
        {
            var positionService = new PositionService(CompoundDb.Compound());

            return await positionService.AddAsync(
                positionModel.Name,
                cancellationToken);
        }

        [HttpPost]
        public async Task<PositionModel> SelectingPosition(int id)
        {
            var positionService = new PositionService(CompoundDb.Compound());

            var position = await positionService.SelectingAsync(id);

            return new PositionModel()
            {
                Id = position.Id,
                Name = position.Name
            };
        }

        [HttpPost]
        public async Task UpdatePosition(
            PositionModel positionModel,
            int id,
            CancellationToken cancellationToken)
        {
            var positionService = new PositionService(CompoundDb.Compound());

            var position = await positionService.SelectingAsync(id);

            await positionService.UpdateAsync(
                position,
                positionModel.Name,
                cancellationToken);
        }

        [HttpPost]
        public async Task DeletePosition(int id, CancellationToken cancellationToken)
        {
            var positionService = new PositionService(CompoundDb.Compound());

            var position = await positionService.SelectingAsync(id);

            await positionService.DeleteAsync(position, cancellationToken);
        }
    }
}
