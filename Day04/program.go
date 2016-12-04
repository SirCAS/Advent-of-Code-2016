package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"sort"
	"strconv"
	"strings"
)

func run() int {
	if len(os.Args) != 3 {
		fmt.Fprintf(os.Stderr, "%s [filename] [search term]\n", os.Args[0])
		return 1
	}

	file, err := os.Open(os.Args[1])
	if err != nil {
		fmt.Fprintln(os.Stderr, err)
		return 1
	}
	defer file.Close()

	searchTerm := os.Args[2]

	validLines := 0

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := strings.Split(scanner.Text(), "-")
		lastPart := strings.Split(line[len(line)-1], "[")

		encryptedName := strings.Join(line[:len(line)-1], " ")
		sectorID, _ := strconv.Atoi(lastPart[0])
		checksum := lastPart[1][:len(lastPart[1])-1]

		if validateChecksum(encryptedName, checksum) {
			validLines += sectorID
			decryptedName := decrypt(encryptedName, sectorID)

			if strings.Contains(decryptedName, searchTerm) {
				fmt.Println(sectorID, ":", decryptedName)
			}
		}
	}

	if err := scanner.Err(); err != nil {
		log.Fatal(err)
	}

	fmt.Println("The sum of valid lines is", validLines)

	return 0
}

func decrypt(input string, iterations int) string {
	result := ""
	for _, char := range input {
		for i := 0; i < iterations; i++ {
			if char+1 > 'z' {
				char = 'a'
			} else if char != ' ' {
				char++
			}
		}

		result = result + string(char)
	}
	return result
}

func validateChecksum(room string, checksum string) bool {
	// Find unique runes in strings and count the occurence
	m := make(map[rune]int)
	for _, char := range room {
		if char != ' ' {
			m[char]++
		}
	}

	// golang doesnt support sorting maps directingly, thus we'll do this
	// TODO: Investigate if this could be done smarter
	pl := make(pairList, len(m))
	i := 0
	for k, v := range m {
		pl[i] = pair{k, v}
		i++
	}
	sort.Sort(sort.Reverse(pairList(pl)))

	for key, value := range pl {
		if key > 4 {
			break
		}

		if rune(checksum[key]) != value.Key {
			return false
		}
	}

	return true
}

type pair struct {
	Key   rune
	Value int
}

type pairList []pair

func (p pairList) Len() int {
	return len(p)
}

func (p pairList) Less(i, j int) bool {
	if p[i].Value != p[j].Value {
		return p[i].Value < p[j].Value
	}

	return p[i].Key > p[j].Key
}

func (p pairList) Swap(i, j int) {
	p[i], p[j] = p[j], p[i]
}

func main() {
	fmt.Println("+-------------------------+")
	fmt.Println("| Advent of Code - Day 04 |")
	fmt.Println("+-------------------------+")

	result := run()
	os.Exit(result)
}
