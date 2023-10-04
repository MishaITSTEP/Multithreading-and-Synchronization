using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Path = System.IO.Path;

namespace Паралельне_програмування
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
        private AnalysisResult analysisresult { get; set; } = new AnalysisResult();
        private CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        private bool isMessage { get; set; } = true;
        public string path => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"AnalysisTextResults.json");

        CancellationToken token => CancellationTokenSource.Token;
        public string ThenText { get; set; } = "";
        public ObservableCollection<AnalysisResult> analysisresults { get; set; } = new ObservableCollection<AnalysisResult>();
        public ViewModel()
        {
            analysisresults=
               File.Exists(path)
               ? JsonConvert.DeserializeObject<ObservableCollection<AnalysisResult>>(File.ReadAllText(path,Encoding.Unicode))
               : new ObservableCollection<AnalysisResult>();
        }

        public async void Analis(string path = null)
        {
            try
            {
                Task<AnalysisResult> task =
            new Task<AnalysisResult>(
                () =>
                {
                    AnalysisResult res = new AnalysisResult();
                    Task[] task = new Task[] {
                        new Task(() => res.Sentences=ThenText.Split(".!?".ToArray(),StringSplitOptions.RemoveEmptyEntries).Count(),token),
                        new Task(() => res.InterrogativeSentences=ThenText.Split("?".ToArray(),StringSplitOptions.RemoveEmptyEntries).Count(),token),
                        new Task(() => res.ExclamatorySentences=ThenText.Split("!".ToArray(),StringSplitOptions.RemoveEmptyEntries).Count(),token),
                        new Task(() => res.Characters=ThenText.Count(),token),
                        new Task(() => res.Words=ThenText.Split(" \task\n\res".ToArray(),StringSplitOptions.RemoveEmptyEntries).Count(),token)
                    };
                    Parallel.ForEach(task,x => x.Start());
                    Task.WaitAll(task);
                    return res;
                });

                Task<AnalysisResult> t = task;
                t.Start();
                t.Wait();
                var r = t.Result;
                if(token.IsCancellationRequested)
                    return;
                if(isMessage)
                {
                    MessageBox.Show(analysisresult.ToString);
                } else
                {
                    analysisresults.Add(analysisresult);
                    File.WriteAllText(path,JsonConvert.SerializeObject(analysisresults),Encoding.Unicode);
                }

            } catch(Exception e)
            {
                MessageBox.Show(e.Message);
            } finally
            {
                if(token.IsCancellationRequested)
                    CancellationTokenSource.Dispose();
            }
        }
        public void Cancel() => CancellationTokenSource.Cancel();
    }
}