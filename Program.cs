using NAudio.Wave;
using NAudio.Wave.SampleProviders;

var sineOne = new SignalGenerator() { 
	Gain = 0.2, 
	Frequency = 220,
	Type = SignalGeneratorType.Sin}
	.Take(TimeSpan.FromSeconds(1));

var sineTwo = new SignalGenerator() { 
	Gain = 0.2, 
	Frequency = 330,
	Type = SignalGeneratorType.Sin}
	.Take(TimeSpan.FromSeconds(1));

using (var woA = new WaveOutEvent())
using (var woB = new WaveOutEvent())
{
	woA.Init(sineOne);
	woB.Init(sineTwo);
	woA.Play();
	woB.Play();
	while (woA.PlaybackState == PlaybackState.Playing && woB.PlaybackState == PlaybackState.Playing) {
		Thread.Sleep(500);
	}
}