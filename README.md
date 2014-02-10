# Git jest git

## Wstęp

- git łatwym nie jest
- ale warto!

Aby poznać/używać gita trzeba wiedzieć chociaż trochę o tym jak działa w środku:

- [UX mogłoby być lepsze](http://stevelosh.com/blog/2013/04/git-koans/), ponadto generalnie w DVCS te same komendy co w SVN/CVS znaczą co innego
- normalnie w takim przypadku pojawia się lamka ostrzegawcza: nie używać!
- ale bebechy git-a są tak dobre... i [proste](http://johannesbrodwall.com/2013/10/09/having-fun-with-git/), że nie jest to problemem
- w tym przypadku UI to nie wszystko i odrobina nauki popłaca

[Statystyki użycia systemów zarządzania źródłami (*nie* tylko open-source)](http://www.slideshare.net/IanSkerrett/eclipse-survey-2013-report-final/14)

## DEMO 1

### Podstawy

	// tworzymy nowe repozytorium
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

## Przegląd podstawowych operacji

### Jak się poruszać

	> git checkout feature
	> git checkout master
	> git checkout abcd...
	> git checkout HEAD^

"HEAD^" to przykład referencji. Inne: ID, rodzice (I^1), tag, branch, ^n...

![http://geek-and-poke.com/geekandpoke/2013/11/7/the-ultimative-geek-speak-quiz](http://static.squarespace.com/static/518f5d62e4b075248d6a3f90/t/527c1880e4b073edc70a9dd6/1383864462509/geek-speak.jpg?format=500w)


### Jak się wycofać

Gdy chcemy wyczyścić (porzucić nieskomitowane zmiany)

	// wycofanie ostatnie commita "undo"
	> git reset --soft HEAD^
	// wycofanie i porzucenie zmian
	> git reset --hard HEAD^
	// usunięcie nie śledzonych plików (nie dodanych do repo)
	> git clean

### Szybkie poprawki

...

### Scalanie

...

### Staging

...

### Podsumowanie: podstawowe operacje

- *init, clone* (rozruch)
- *add, commit* (ora et labora)
- *branch, merge* (wycisz się, odizoluj, znajdź swoje "pudełko nicości")
- *diff, log, status* (What's up doc?)
- *checkout* (Where do you want to go today?)
- *push, pull* (podziel się)

## Bibliografia

- biblia [Pro Git](http://git-scm.com/book)
- [ściąga](http://ndpsoftware.com/git-cheatsheet.html)
- [PoshGit: PowerShell environment for Git](https://github.com/dahlbyk/posh-git)
- [SourceTree](http://www.sourcetreeapp.com/)
- [wstęp do git-svn](http://www.benedykt.net/2013/03/22/kodzic-po-ludzku-czyli-jak-sie-pozbyc-svn-a/)
- [więcej...](https://pinboard.in/u:orientman/t:Git/)

TBC...
