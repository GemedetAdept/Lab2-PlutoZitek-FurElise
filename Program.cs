using NAudio.Wave;
using NAudio.Wave.SampleProviders;

var sineOne = new SignalGenerator() { 
	Gain = 0.2, 
	Frequency = 220,
	Type = SignalGeneratorType.Sin}
	.Take(TimeSpan.FromSeconds(2));

var sineTwo = new SignalGenerator() { 
	Gain = 0.2, 
	Frequency = 330,
	Type = SignalGeneratorType.Sin}
	.Take(TimeSpan.FromSeconds(2));

var sineThree = new SignalGenerator() { 
	Gain = 0.2, 
	Frequency = 262,
	Type = SignalGeneratorType.Sin}
	.Take(TimeSpan.FromSeconds(2));

using (var woA = new WaveOutEvent())
using (var woB = new WaveOutEvent())
using (var woC = new WaveOutEvent())
{
	woA.Init(sineOne);
	woB.Init(sineTwo);
	woC.Init(sineThree);
	woA.Play();
	woB.Play();
	woC.Play();
	while (woA.PlaybackState == PlaybackState.Playing && woB.PlaybackState == PlaybackState.Playing) {
		Thread.Sleep(500);
	}
}