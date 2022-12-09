using NAudio.Wave;
using NAudio.Wave.SampleProviders;

List<int> noteDurations = new List<int>()
	{2, 2, 2};
List<string> noteNames = new List<string>()
	{"A~", "E~", "C~"};
List<int> noteOctaves = new List<int>()
	{3, 4, 4};

List<MusicalNote> notesList = new List<MusicalNote>();
for (int i=0; i < noteDurations.Count; i++) {
	MusicalNote newNote = new MusicalNote(
		noteDurations[i],
		noteNames[i],
		noteOctaves[i]
	);

	Console.WriteLine($"{newNote.NoteLetter}{newNote.Octave}, {newNote.Frequency}");

	notesList.Add(newNote);
}

var noteSignals = notesList.Select( x =>
	new SignalGenerator() { 
		Gain = 0.2, 
		Frequency = x.Frequency, // These are doubles!!!
		Type = SignalGeneratorType.Sin}
		.Take(TimeSpan.FromSeconds(x.Duration))
).ToList();

void playNotes() {
	using (var woA = new WaveOutEvent())
	using (var woB = new WaveOutEvent())
	using (var woC = new WaveOutEvent())
	{
		woA.Init(noteSignals[0]);
		woB.Init(noteSignals[1]);
		woC.Init(noteSignals[2]);
		woA.Play();
		woB.Play();
		woC.Play();
		while (woA.PlaybackState == PlaybackState.Playing && woB.PlaybackState == PlaybackState.Playing) {
			Thread.Sleep(500);
		}
	}
}

playNotes();