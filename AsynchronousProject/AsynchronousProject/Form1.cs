using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsynchronousProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Task.WhenAll(UpdateProgressBarAsync(progressBar1, 100),
                               UpdateProgressBarAsync(progressBar2, 150),
                               UpdateProgressBarAsync(progressBar3, 200));

            MessageBox.Show("작업이 완료되었습니다.");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await Task.Run(() => this.BeginInvoke(new Action(() => UpdateProgressBarAsync(progressBar1, 100))));
            await Task.Run(() => this.BeginInvoke(new Action(() => UpdateProgressBarAsync(progressBar2, 150))));
            await Task.Run(() => this.BeginInvoke(new Action(() => UpdateProgressBarAsync(progressBar3, 200))));

            MessageBox.Show("작업이 완료되었습니다.");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await UpdateProgressBarAsync(progressBar1, 100);
            await UpdateProgressBarAsync(progressBar2, 150);
            await UpdateProgressBarAsync(progressBar3, 200);

            MessageBox.Show("작업이 완료되었습니다.");
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            // Task를 직접 생성하여 병렬로 실행
            var task1 = UpdateProgressBarAsync(progressBar1, 100);
            var task2 = UpdateProgressBarAsync(progressBar2, 150);
            var task3 = UpdateProgressBarAsync(progressBar3, 200);

            // 모든 Task가 완료될 때까지 대기
            await Task.WhenAll(task1, task2, task3);

            MessageBox.Show("작업이 완료되었습니다.");
        }

        private async Task UpdateProgressBarAsync(ProgressBar progressBar, int maxValue)
        {
            // 프로그레스바 초기화
            progressBar.Value = 0;
            progressBar.Maximum = maxValue;

            // 비동기 작업 시뮬레이션
            for (int i = 0; i <= maxValue; i++)
            {
                // 10밀리초마다 대기하며 프로그레스바 갱신
                await Task.Delay(10);
                progressBar.Value = i;
            }
        }
    }
}
