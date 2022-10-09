using System.Diagnostics;
using CustomTask.Tasks;
using NUnit.Framework;

namespace CustomTaskTests;


public class TestAwaiter
{
    [Test]
    public async Task TestAwait()
    {
        var command = new ResultSimpleTask();
        new Thread(() =>
        {
            Thread.Sleep(1000);
            command.Complete(new TaskResult(true));
        }).Start();
        var watch = new Stopwatch();
        watch.Start();
        var res = await command.Wait();
        watch.Stop();
        Assert.AreEqual(res.IsSuccess, true);
        Assert.GreaterOrEqual(watch.ElapsedMilliseconds, 900);
    }

    [Test]
    public async Task TestAwaitTimeoutTask()
    {
        var command = new TimeoutResultSimpleTask();
        new Thread(() =>
        {
            Thread.Sleep(1000);
            command.Complete(new TaskResult(true));
        }).Start();
        var watch = new Stopwatch();
        watch.Start();
        var res = await command.Wait(10000);
        watch.Stop();
        Assert.AreEqual(res.IsSuccess, true);
        Assert.GreaterOrEqual(watch.ElapsedMilliseconds, 900);
        Assert.GreaterOrEqual(9000, watch.ElapsedMilliseconds);
    }

    [Test]
    public async Task TestAwaitTimeoutRun()
    {
        var command = new TimeoutResultSimpleTask();
        new Thread(() =>
        {
            Thread.Sleep(10000);
            command.Complete(new TaskResult(true));
        }).Start();
        var watch = new Stopwatch();
        watch.Start();
        var res = await command.Wait(1000);
        watch.Stop();
        Assert.AreEqual(res.IsSuccess, false);
        Assert.GreaterOrEqual(watch.ElapsedMilliseconds, 900);
        Assert.GreaterOrEqual(9000, watch.ElapsedMilliseconds);
    }
}