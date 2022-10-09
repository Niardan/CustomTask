using System.Diagnostics;
using CustomTask.Tasks;
using NUnit.Framework;

namespace CustomTask.Tests;

[TestFixture]
public class TestAwaiter
{
    [Test]
    public async Task TestSimpleTaskAwait()
    {
        var command = new SimpleTask<bool>();
        new Thread(() =>
        {
            Thread.Sleep(1000);
            command.Complete(true);
        }).Start();
        var watch = new Stopwatch();
        watch.Start();
        var res = await command.Wait();
        watch.Stop();
        Assert.AreEqual(res, true);
        Assert.GreaterOrEqual(watch.ElapsedMilliseconds, 900);
    }

    [Test]
    public async Task TestSimpleTaskTimeoutRun()
    {
        var command = new SimpleTask<bool>();
        new Thread(() =>
        {
            Thread.Sleep(1000);
            command.Complete(true);
        }).Start();
        var watch = new Stopwatch();
        watch.Start();
        var res = await command.Wait(10000);
        watch.Stop();
        Assert.AreEqual(res, true);
        Assert.GreaterOrEqual(watch.ElapsedMilliseconds, 900);
        Assert.GreaterOrEqual(9000, watch.ElapsedMilliseconds);
    }

    [Test]
    public void TestSimpleTaskTimeoutWait()
    {
        var command = new SimpleTask<bool>();
        new Thread(() =>
        {
            Thread.Sleep(10000);
            command.Complete(true);
        }).Start();
       Assert.ThrowsAsync<TimeoutException>(async () => await command.Wait(1000));
    }

    [Test]
    public async Task TestResultTask()
    {
        var command = new ResultSimpleTask();
        new Thread(() =>
        {
            Thread.Sleep(1000);
            command.Complete(true, "successful");
        }).Start();
        var watch = new Stopwatch();
        watch.Start();
        var res = await command.Wait();
        watch.Stop();
        Assert.AreEqual(res.IsSuccess, true);
        Assert.AreEqual(res.Log, "successful");
        Assert.GreaterOrEqual(watch.ElapsedMilliseconds, 900);
    }

    [Test]
    public async Task TestAwaitTimeoutTask()
    {
        var command = new ResultSimpleTask();
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
        var command = new ResultSimpleTask();
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
        Assert.AreEqual(res.Log, "timeout");
        Assert.GreaterOrEqual(watch.ElapsedMilliseconds, 900);
        Assert.GreaterOrEqual(9000, watch.ElapsedMilliseconds);
    }
}