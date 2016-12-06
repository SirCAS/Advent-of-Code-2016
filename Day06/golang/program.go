package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
)

func run() int {
	file, err := os.Open("input.txt")
	if err != nil {
		log.Fatal(err)
		return 1
	}
	defer file.Close()

	// Fetch data from file arranged in cols with maps holding a occourences counter
	columns := make(map[int]map[rune]int)
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		for col, char := range scanner.Text() {
			if _, err := columns[col]; !err {
				columns[col] = make(map[rune]int)
			}
			columns[col][char]++
		}
	}

	if err := scanner.Err(); err != nil {
		log.Fatal(err)
		return 1
	}

	// Analyze rows in order to find max and min values in cols
	maxOccurrenceMessage := make([]rune, len(columns))
	minOccurrenceMessage := make([]rune, len(columns))
	for colIndex, col := range columns {
		var max int
		var min int
		for key, value := range col {
			if value > max {
				maxOccurrenceMessage[colIndex] = key
				max = value
			}
			if min == 0 || value < min {
				minOccurrenceMessage[colIndex] = key
				min = value
			}
		}
	}

	fmt.Println("Error-corrected version using most occurrences: ", string(maxOccurrenceMessage))
	fmt.Println("Error-corrected version using least occurrences: ", string(minOccurrenceMessage))

	return 0
}

func main() {
	fmt.Println("+-------------------------+")
	fmt.Println("| Advent of Code - Day 06 |")
	fmt.Println("+-------------------------+")

	result := run()

	fmt.Println("  - Glædelig jul!")

	os.Exit(result)
}
