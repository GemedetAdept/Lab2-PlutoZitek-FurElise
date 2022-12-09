using NAudio.Wave;
using NAudio.Wave.SampleProviders;

// 3/8 time @ 135 tempo
int tempo = (135);
Console.WriteLine(tempo);
MusicalNote.SetTimingProperties(3, 8, tempo);

// Whl, Hlf, Qrt, Egt, Sxt, Ths
// Whole, Half, Quarter, Eighth, Sixteenth, Thirty-Secondth
List<string> noteValues = new List<string>() {
"Sxt", "Sxt", "Sxt", "Sxt", "Sxt", "Sxt", "Sxt", "Sxt", "Sxt","Sxt", "Sxt", "Sxt", "Sxt", "Sxt", 
"Sxt", "Sxt","Sxt", "Sxt", "Sxt", "Sxt", "Sxt", "Sxt", "Sxt","Sxt", "Sxt", "Sxt", "Sxt", "Sxt", "Sxt", 
"Sxt", "Sxt", "Sxt", "Sxt", "Sxt", "Sxt", "Sxt","Sxt", "Sxt", "Sxt", "Sxt", "Sxt", "Sxt", "Sxt","Sxt", 
"Sxt", "Sxt", "Sxt", "Sxt", "Sxt", "Sxt","Sxt", "Sxt", "Sxt", "Egt", "Sxt", "Sxt", "Sxt", 
};

List<string> noteNames = new List<string>() {
"C~", "B~", "C~", "B~", "C~", "G~", "Bb", "Ab", "F~","F~", "C~", "F~", "Ab", "C~", 
"F~", "G~","C~", "C~", "G~", "C~", "G~", "G~", "Ab","F~", "C~", "F~", "C~", "C~", "B~", 
"C~", "B~", "C~", "G~", "Bb", "Ab", "F~","F~", "C~", "F~", "Ab", "C~", "F~", "G~","C~", 
"C~", "G~", "C~", "Ab", "G~", "F~","F~", "C~", "F~", "%%", "G~", "Ab", "B~", 
};
List<int> noteOctaves = new List<int>() {
4, 4, 4, 4, 4, 3, 4, 4, 3,1, 2, 2, 3, 3, 
3, 3,1, 2, 2, 3, 3, 3, 4,1, 2, 2, 3, 4, 4, 
4, 4, 4, 3, 4, 4, 3,1, 2, 2, 3, 3, 3, 3,1, 
2, 2, 3, 4, 3, 3,1, 2, 2, 1, 3, 4, 4, 
};
List<(int, int)> chordIndices = new List<(int, int)>(){
	(9,10), (16,17), (23,24), (36,37), (43,44), (50,51),
};

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
	if (note.NoteLetter == "%%") {note.Frequency = 0;}
	Console.WriteLine($"{note.NoteLetter} {note.Octave}: {note.Duration}, {note.Frequency}");
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
				Thread.Sleep((int)((notesList[0].Duration)*1000));
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
				Thread.Sleep((int)((notesList[noteIndex].Duration)*1000));
				break;
			}
		}
	}
}}

playNotes();