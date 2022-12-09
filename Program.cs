using NAudio.Wave;
using NAudio.Wave.SampleProviders;

// 3/8 time @ 135 tempo
MusicalNote.SetTimingProperties(3, 8, 135);

List<double> noteValues = new List<double>()
	{1, 1,  1,1,1,  1, 1,  1,1,1};
List<string> noteNames = new List<string>()
	{"G~", "G~",  "A~","E~","C~",  "G~", "G~",  "A~","E~","C~",};
List<int> noteOctaves = new List<int>()
	{4, 4,  3,4,4,  3, 3,  3,4,4,};
List<(int, int)> chordIndices = new List<(int, int)>()
	{(2, 4), (7, 9)};

List<MusicalNote> notesList = new List<MusicalNote>();
for (int i=0; i < noteValues.Count; i++) {
	MusicalNote newNote = new MusicalNote(
		noteValues[i],
		noteNames[i],
		noteOctaves[i]
	);

	notesList.Add(newNote);
}

foreach (MusicalNote note in notesList) {
	Console.WriteLine(note.Duration);
}

var noteSignals = notesList.Select( x =>
	new SignalGenerator() { 
		Gain = 0.1, 
		Frequency = x.Frequency,
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
	
		using (useWOE.Select(x => x) as IDisposable) {
	
			for (int i=0; i < chordSignals.Count; i++) {
				useWOE[i].Init(chordSignals[i]);
			}
			for (int i=0; i < chordSignals.Count; i++) {
				useWOE[i].Play();
			}
			while (useWOE[0].PlaybackState == PlaybackState.Playing) {
				Thread.Sleep((int)((noteValues[0]*Math.Pow(10,3))));
				break;
			}
		}

		nextChord++;
	}

	else {
		using (var useWOE = new WaveOutEvent()) {

			useWOE.Init(noteSignals[noteIndex]);
			useWOE.Play();
			while (useWOE.PlaybackState == PlaybackState.Playing) {
				Thread.Sleep((int)(noteValues[noteIndex]*Math.Pow(10,3)));
				break;
			}
		}
	}
}}

// playNotes();