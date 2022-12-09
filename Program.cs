using NAudio.Wave;
using NAudio.Wave.SampleProviders;

List<double> noteDurations = new List<double>()
	{0.2, 0.05, 0.2};
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
	for (int i=0; i < noteSignals.Count; i++) {
		using (var outputNote = new WaveOutEvent()) {
			outputNote.Init(noteSignals[i]);
			outputNote.Play();

			while (outputNote.PlaybackState == PlaybackState.Playing) {
				Thread.Sleep((int)(noteDurations[i]*Math.Pow(10,3)));
			}
		}
	}
}

playNotes();