Простая реализация асинхронного ожидания с ручным вызовом "Complete" и с таймаутом.
Две реализации:
Типизированная SimpleTask, использование:
 var command = new SimpleTask<bool>();
        new Thread(() =>
        {
            Thread.Sleep(1000);
            command.Complete(true);
        }).Start();       
        var res = await command.Wait();    
        
В случае таймаута кидает Exception.

Чуть более общая ResultSimpleTask:
Возвращает TaskResult с булевым флагом и строковым параметром с логами.

  var command = new ResultSimpleTask();
        new Thread(() =>
        {
            Thread.Sleep(1000);
            command.Complete(true, "successful");
        }).Start();      
        var res = await command.Wait();     
        Assert.AreEqual(res.IsSuccess, true);
        Assert.AreEqual(res.Log, "successful");       
		
В случае таймаута возвращает False  с текстом "timeout"