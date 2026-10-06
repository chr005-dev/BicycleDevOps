using BicycleApi.Models;

namespace BicycleApi.Services;

public class BicycleService
{
    private readonly object _gate = new();

    private readonly List<Bicycle> _bicycles =
    [
        new(1, "Trek", 4500m),
        new(2, "Giant", 3200m)
    ];

    private int _nextId = 3;

    public IReadOnlyList<Bicycle> GetAll()
    {
        lock (_gate)
        {
            return _bicycles.ToArray();
        }
    }

    public Bicycle? GetById(int id)
    {
        lock (_gate)
        {
            return _bicycles.FirstOrDefault(b => b.Id == id);
        }
    }

    public Bicycle Add(string brand, decimal price)
    {
        if (string.IsNullOrWhiteSpace(brand))
        {
            throw new ArgumentException(
                "Mærket skal udfyldes.", nameof(brand));
        }

        if (price <= 0 || price > 1000000m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(price),
                "Prisen skal være mellem 0,01 og 1.000.000.");
        }

        lock (_gate)
        {
            var bicycle = new Bicycle(
                _nextId++, brand.Trim(), price);

            _bicycles.Add(bicycle);

            return bicycle;
        }
    }
}