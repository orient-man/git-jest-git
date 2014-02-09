# Git jest git

## Wstęp

- git is hard
- ale warto!

Aby poznać/używać gita trzeba wiedzieć chociaż trochę o tym jak działa w środku:

- normalnie jest lamka ostrzegawcza: nie używać!
- ale bebechy git-a są tak dobre... i proste, że nie jest to problemem
- w tym przypadku UI to nie wszystko i odrobina nauki popłaca

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

TBC...
