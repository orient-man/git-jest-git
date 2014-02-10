# Git jest git

## Wstęp

- git łatwym nie jest
- ale warto!

Aby poznać/używać gita trzeba wiedzieć chociaż trochę o tym jak działa w środku:

- [UX mogłoby być lepsze](http://stevelosh.com/blog/2013/04/git-koans/)
- normalnie w takim przypadku pojawia się lamka ostrzegawcza: nie używać!
- ale bebechy git-a są tak dobre... i proste, że nie jest to problemem
- w tym przypadku UI to nie wszystko i odrobina nauki popłaca

[Statystyki użycia systemów zarządzania źródłami (*nie* tylko open-source)](http://www.slideshare.net/IanSkerrett/eclipse-survey-2013-report-final/14)

## DEMO

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

Dwie strategie: merge lub rebase (git-svn). Branch to *tylko* etykieta. Scalenie zmian to najczęściej jedynie przesunięcie tej etykiety!

### Typowy workflow: Isolate -> Work <-> Update -> Share 

- wydziel sobie miejsce do pracy w postaci gałęzi (branch)
- pracuj (add, commit)
- zaktualizuj i przetestuj (update, merge, rebase)
- podziel się (merge -> test -> push)

### Jak się poruszać

	> git checkout HEAD^

"HEAD^" to przykład referencji. Inne: ID, rodzice (I^1), tag, branch, ^n...

![http://geek-and-poke.com/geekandpoke/2013/11/7/the-ultimative-geek-speak-quiz](http://static.squarespace.com/static/518f5d62e4b075248d6a3f90/t/527c1880e4b073edc70a9dd6/1383864462509/geek-speak.jpg?format=500w)

### Jak się wycofać

Gdy chcemy wyczyścić (porzucić nieskomitowane zmiany)

	> git reset --hard HEAD^
	> git clean

### Scalanie

### Staging


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
