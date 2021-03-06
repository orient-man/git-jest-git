# Git jest git

## Wstęp

- git łatwym nie jest
- ale warto!

Aby poznać/używać gita trzeba wiedzieć chociaż trochę o tym jak działa w środku:

- [UX mogłoby być lepsze](http://stevelosh.com/blog/2013/04/git-koans/), ponadto generalnie w DVCS te same komendy co w SVN/CVS znaczą co innego
- normalnie w takim przypadku pojawia się lamka ostrzegawcza: nie używać!
- ale bebechy git-a są tak dobre... i [proste](./Quine/Program.cs), że nie jest to problemem
- w tym przypadku UI to nie wszystko i odrobina nauki popłaca

[Statystyki użycia systemów zarządzania źródłami (*nie* tylko open-source)](http://www.slideshare.net/IanSkerrett/eclipse-survey-2013-report-final/14)

## DEMO 1

### Podstawy

	// tworzymy nowe repozytorium
	> mkdir test
    > git init
    // zawartość repo (głównie puste katalogi)
    > tree /F .git
    > vim foo
    > git add foo
    > git commit
    // ...kolejna zmiana, kolejny commit
    > git log

Format ID = SHA1(content, autor, data, log, ID poprzedniego commita). Jak to wygląda w [GitViz](https://github.com/Readify/GitViz).


	> git branch feature
	> git checkout feature
	// skrót: git checkout -b feature
	... add, commit
	> git merge master		// git rebase master
	// test czy wszystko działa
	> git checkout master
	// fast-forward merge
	> git merge feature

Branch to *tylko* etykieta. Scalenie zmian to najczęściej jedynie przesunięcie tej etykiety! Proste i bezpieczne.

To był 1 z 2 możliwych wariantów scalania. Drugi to *rebase* pozwalający wyprostować historię:

	> git checkout feature2
	... add, commit
	> git checkout master
	... add, commit
	> git checkout feature2
	> git rebase master
	// i testujemy czy działa
	> git checkout master
	// już tylko fast-forward 
	> git merge --ff-only feature

Wybór jest filozoficzny... (patrz przykładowe repo EventStore). Chyba, że używamy git-svn - wtedy nie mamy wyboru i trzeba robić rebase.
 
## Typowy workflow: Isolate -> Work <-> Update -> Share 

- wydziel sobie miejsce do pracy w postaci gałęzi (branch)
- pracuj (add, commit)
- zaktualizuj i przetestuj (update, merge, rebase)
- podziel się (merge -> test -> push)

## DEMO 2

Praca ze "zdalnymi" repozytoriami:

	> git clone --bare test/ remote/
	> git clone remote/ alice/
	> git clone remote/ bob/
	> cd alice
	> git config --local user.name alice
	.. add, commit foo
	> git push
	> cd ../bob/
	> git config --local user.name bob
	.. add, commit foo
	> git fetch
	> git pull
	// BUM! konflikt
	> git mergetool
	> git commit
	> git push
	> cd ../alice/
	> git pull

Generalnie konflikt to rzadko występujący zwierz, bo git ma dobry algorytm trójstronny (zna wspólnego rodzica). Merge(tool):

 - domyślnie TortoiseMerge
 - ja robię to ręcznie edytorem

Co śledzi co:

	> git branch -vv

"Rex, podaj gałąź":

	> git checkout -b from-alice-with-love
	.. add, commit
	> git push --set-upstream origin from-alice-with-love
	> cd ../bob/
	> git fetch
	> git checkout origin/from-alice-with-love
	// jeśli chcemy commitować to powinniśmy zrobić lokalną gałąź
	> git branch from-bob-to-alice
	> git branch --set-upstream-to origin/from-alice-with-love
	...

Uwagi:

 - ID (SHA1) nie tylko unikalnie identyfikuje commit, ale całą jego historię - stąd możliwy jest szybki protokół sieciowy
 
## Przegląd podstawowych operacji

### Jak się poruszać

	> git checkout feature
	> git checkout master
	> git checkout sha1
	> git checkout HEAD^
	> git checkout -- path
	> git checkout branch -- path

"HEAD^" to przykład referencji. Inne: ID, rodzice (I^1), tag, branch, ^n...

![http://geek-and-poke.com/geekandpoke/2013/11/7/the-ultimative-geek-speak-quiz](./geek-speak.jpg)

### Jak się wycofać

Gdy chcemy wyczyścić (porzucić nieskomitowane zmiany)

	// wycofanie ostatnie commita "undo"
	> git reset --soft HEAD^
	// wycofanie i porzucenie zmian
	> git reset --hard HEAD^
	// usunięcie nie śledzonych plików (nie dodanych do repo)
	> git clean
	// popraw ostatni commit
	> git commit --amend

### Staging

Wybieramy niczym wybredny klient w restauracji:

	> vim foo
	// poprawiamy literówkę i coś jeszcze
	> vim bar
	> git add -p bar
	> git commit -m "Literówka"
	> git add -A
	> git commit

Usuwanie z indeksu:

	> git reset HEAD -- path-to-file

### Stashing

Muszę szybko zmienić kontekst, a nie chce mi się teraz myśleć... weź gdzieś to zapisz:

	// --include-untracked
	> git stash -u

Chcę przetestować to co zamierzam skomitować:

	// --keep-index
	> git stash -k

### Poprawianie historii

Commit jest niemodyfikowalny, zatem historii nie da się poprawić... ale można napisać ją od nowa:

	> git rebase -i

Zmiany w SCM powinny opisywać historię naszego projektu. I dlatego trzeba tę historię poprawiać :). Pracuję cały dzień i commituję chaotycznie - na koniec dnia prostuję i wysyłam do centralnego repo.

### Podsumowanie: podstawowe operacje

- *init, clone* (rozruch)
- *add, commit* (ora et labora)
- *branch, merge* (wycisz się, odizoluj, znajdź swoje "pudełko nicości")
- *diff, log, status* (What's up doc?)
- *checkout* (Where do you want to go today?)
- *push, pull* (podziel się)

![https://twitter.com/flowchainsensei/status/408167162344648704/photo/1](./wisdom.jpg)

## Git vs. Subversion

Git czyli "luz w...":

- komitujemy ile chcecemy
- pracujemy nad czym chcemy (gałęzie) - a nie "mam rozgrzebane, więc chwilowo nie mogę..."
- możemy trzymać w repozytorium wszystko, także eksperymenty - a może się przydadzą?
- commit jest święty i historia jest święta... ale można napisać ją od nowa
- i ta historia może składać się w sensowną opowieść o naszym kodzie (skąd się wzięła dana zmiana, jaki ciąg zmian składa się na daną funkcjonalność etc.)
- pełna kontrola nad synchronizacją z innymi
 - krok 1: "u mnie działa" -> commit
 - krok 2: sync -> nie działa? -> poprawki -> commit
 - krok 3: podziel się
- każdy ma pełną kopię historii (backup)
- można pracować całkowicie lokalnie (bez sieci)
- centralny f**kup na nie blokuje (np.: ktoś coś wrzucił co się nie kompiluje i poszedł do domu)

SVN to "pain in the...":

- w svn commit jest operację **groźną**:
 - wymaga kontaktu z serwerem
 - wymaga synchronizacji z innymi
 - ta synchronizacja może nam nieodwracalanie namieszać w kopii roboczej
- w efekcie komitujemy rzadziej niż powinniśmy
- a przy trudnych zmianach w ogóle przez dłuższy czas (godziny, dni)
- czyli nie korzystamy z svn :)
- teoretycznie można by sobie z tym poradzić i temu służą gałęzie, ale...
- gałęzie w svn są... **ciężkie** i trudne w obsłudze

Ponadto:

- git jest bardzo szybki
- git otwiera nas na nowe sposoby pracy
 - svn z perspektywy git nie różni się znacząco od zwykłego backupu
- git jest git

## Bonusy

- budowa repo
- no renames
- bisect
- autor vs. komitujący

...

## Bibliografia

- biblia [Pro Git](http://git-scm.com/book)
- [ściąga](http://ndpsoftware.com/git-cheatsheet.html)
- [PoshGit: PowerShell environment for Git](https://github.com/dahlbyk/posh-git)
- [SourceTree](http://www.sourcetreeapp.com/)
- [wstęp do git-svn](http://www.benedykt.net/2013/03/22/kodzic-po-ludzku-czyli-jak-sie-pozbyc-svn-a/)
- [przykład praktyczny p.t. "ok, znam podstawowe operacje i co dalej?"](http://nvie.com/posts/a-successful-git-branching-model/)
- repozytoria w chmurze: [BitBucket](https://bitbucket.org/) i [GitHub](https://github.com/)
- [więcej...](https://pinboard.in/u:orientman/t:Git/)

TBC...
