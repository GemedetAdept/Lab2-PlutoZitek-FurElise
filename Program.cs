using NAudio.Wave;
using NAudio.Wave.SampleProviders;

List<double> noteDurations = new List<double>()
	{1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
List<string> noteNames = new List<string>()
	{"G~", "G~", "A~", "E~", "C~", "G~", "G~", "A~", "E~", "C~",};
List<int> noteOctaves = new List<int>()
	{4, 4, 3, 4, 4, 3, 3, 3, 4, 4,};
List<(int, int)> chordIndices = new List<(int, int)>()
	{(2, 4), (7, 9)};

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
		Gain = 0.1, 
		Frequency = x.Frequency, // These are doubles!!!
		Type = SignalGeneratorType.Sin}
		.Take(TimeSpan.FromSeconds(x.Duration))
).ToList();

void playNotes() {
int chordCount = chordIndices.Count;
int nextChord = 0;
for (int noteIndex=0; noteIndex < notesList.Count; noteIndex++) {

	if (chordCount > 0 && nextChord <= chordCount-1 && noteIndex == chordIndices[nextChord].Item1){

		int start = chordIndices[nextChord].Item1;
		int end = chordIndices[nextChord].Item2;
		int range = end-start+1;

		var chordSignals = noteSignals.Skip(start).Take(range).Select(x => x).ToList();
		var useWOE = chordSignals.Select(x => new WaveOutEvent()).ToList();
		// values1NotAtIndexes = values1.Where((m, index) => !indexes.Contains(index)).ToList();
	
		using (useWOE.Select(x => x) as IDisposable) {
	
			for (int i=0; i < chordSignals.Count; i++) {
				useWOE[i].Init(chordSignals[i]);
			}
			for (int i=0; i < chordSignals.Count; i++) {
				useWOE[i].Play();
			}
			while (useWOE[0].PlaybackState == PlaybackState.Playing) {
				Thread.Sleep(100);
				// Thread.Sleep((int)(noteDurations[0]*Math.Pow(10,3)));

			}
		}

		nextChord++;
	}

	else {
		using (var useWOE = new WaveOutEvent()) {

			useWOE.Init(noteSignals[noteIndex]);
			useWOE.Play();
			while (useWOE.PlaybackState == PlaybackState.Playing) {
				Thread.Sleep(500);
				// Thread.Sleep((int)(noteDurations[noteIndex]*Math.Pow(10,3)));
			}
		}
	}
}}

playNotes();