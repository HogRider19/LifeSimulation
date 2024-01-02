using Core.Logics.Base;
using Core.Logics.Base.Interfaces;
using Core.Models.Base.Interfaces;

namespace Core.Logics;

public class PerformanceLogRule : SimulationRule
{
    private readonly string _filePath;
    private List<long> _buffer = new List<long>(100);
    private readonly int _writePeriod;

    public PerformanceLogRule(string filePath, int writePeriod = 1000) : base()
    {
        if (!File.Exists(filePath))
            throw new ArgumentException("Invalid file path");

        _writePeriod = writePeriod;
        _filePath = filePath;
    }

    public override bool Apply(ISimulationSpace space, ISimulationState state)
    {
        var context = state.GetRuleContext(this);
        var currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var prevTime = (long)context.GetValueOrDefault("prevTime", currentTime);

        var delta = currentTime - prevTime;
        if (delta != 0) LogValueToFile(delta);
        
        context["prevTime"] = currentTime;
        return true;
    }

    private void LogValueToFile(long value)
    {
        if (_buffer.Count > _writePeriod)
        {
            using (var writer = new StreamWriter(_filePath, false))
            {
                foreach (var bufValue in _buffer)
                {
                    writer.WriteLine(bufValue);   
                }
            }
            _buffer.Clear();
        }
        else
            _buffer.Add(value);
    }
}