using devsuApi.Context;

namespace devsuApi.Modules.Movements
{
    public class Services : Contracts
    {
        public DevsudbContext _db;
        public Services(DevsudbContext db)
        {
            _db = db;
        }

        public Task<Movement> CreateMovementAsync(NewMovementModel request)
        {
            Account account = _db.Accounts.Where(x => x.Number == request.AccountNumber).FirstOrDefault()!;
            string validation = ValidateMovement(request, account);
            Movement newMovement = new Movement();
            switch (validation)
            {
                case "Movimiento válido":
                    newMovement = new Movement();
                    newMovement.Amount = request.Amount;
                    newMovement.Date = DateTime.Now;
                    newMovement.Description = request.Description;
                    newMovement.Type = request.Amount > 0 ? "Depósito" : "Retiro";
                    newMovement.AccountId = account.AccountId;
                    newMovement.Balance = account.Balance + request.Amount;
                    newMovement = _db.Movements.Update(newMovement).Entity;
                    account.Balance += request.Amount;
                    _db.SaveChanges();
                    break;
                case "Movimiento no válido":
                    newMovement.Description = "Movimiento no válido";
                    break;
                case "Cupo diario excedido":
                    newMovement.Description = "Cupo diario excedido";
                    break;
            }
            return Task.FromResult(newMovement);
        }

        private string ValidateMovement(NewMovementModel request, Account account)
        {
            if (request.AccountNumber <= 0 || account == null || account.Balance + request.Amount < 0 || account.Balance == 0)
            {
                return "Movimiento no válido";
            }
            else
            {
                List<Movement> movements = _db.Movements.Where(x => x.Date == DateTime.Now.Date && x.AccountId == account.AccountId).ToList();
                long totalPerDay = movements.Sum(x => x.Amount);
                if (totalPerDay + request.Amount > 1000)
                {
                    return "Cupo diario excedido";
                }
                else
                {
                    return "Movimiento válido";
                }
            }
        }

        public Task<bool> DeleteMovementAsync(int id)
        {
            Movement movement = _db.Movements.Find(id)!;
            if (movement != null)
            {
                _db.Movements.Remove(movement);
                _db.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<Movement> GetMovementAsync(int id)
        {
            Movement movement = _db.Movements.Find(id)!;
            return Task.FromResult(movement ?? new Movement());
        }

        public Task<List<Movement>> GetMovementsAsync()
        {
            List<Movement> movements = _db.Movements.ToList();
            return Task.FromResult(movements ?? new List<Movement>());
        }
        public Task<List<Movement>> GetMovementsByAccountAndDateAsync(int id, DateTime date)
        {
            List<Movement> movements = _db.Movements.Where(x => x.AccountId == id && x.Date == date).ToList();

            return Task.FromResult(movements ?? new List<Movement>());
        }
        public Task<Movement> UpdateMovementAsync(int id, NewMovementModel request)
        {
            Account account = _db.Accounts.Where(x => x.Number == request.AccountNumber).FirstOrDefault()!;
            Movement movement = _db.Movements.Find(id)!;
            if (movement != null && account != null)
            {
                movement.Amount = request.Amount;
                movement.Date = DateTime.Now;
                movement.Description = request.Description;
                movement.AccountId = account.AccountId;
                _db.Movements.Update(movement);
                _db.SaveChanges();
                return Task.FromResult(movement);
            }
            else
            {
                return Task.FromResult(new Movement());
            }
        }
    }
}

