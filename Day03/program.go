package main

import (
	"bufio"
	"errors"
	"fmt"
	"log"
	"os"
	"sort"
	"strconv"
	"strings"
)

// ErrInvalidInputFile indicates that the data profiled in the input file is invalid
var ErrInvalidInputFile = errors.New("Invalid input file")

func run() int {
	if len(os.Args) != 2 {
		fmt.Fprintf(os.Stderr, "%s [filename]\n", os.Args[0])
		return 1
	}

	file, err := os.Open(os.Args[1])
	if err != nil {
		fmt.Fprintln(os.Stderr, err)
		return 1
	}
	defer file.Close()

	var lines []string

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		lines = append(lines, scanner.Text())
	}

	if err := scanner.Err(); err != nil {
		log.Fatal(err)
	}

	// Try parsing the first line to get the number of columns/number of angles (e.g. 3=triangle)
	firstLine, err := convertLine(lines[0])
	if err != nil {
		log.Fatal(err)
		log.Fatal("Line number: 0")
		return 1
	}

	numAngles := len(firstLine)

	var goodHorizontal int
	var goodVertically int

	verticalSums := make([][]int, len(lines))

	for i, line := range lines {
		lineValues, err := convertLine(line)
		if err != nil {
			log.Fatal(err)
			log.Fatal("Line number: ", i+1)
			return 1
		}

		// Create copy of un-sorted lineValues for later use
		cpy := make([]int, len(lineValues))
		copy(cpy, lineValues)
		verticalSums[i] = cpy

		if (i+1)%numAngles == 0 {
			for x := 0; x < numAngles; x++ {

				col := []int{}

				for r := 0; r < numAngles; r++ {
					col = append(col, verticalSums[i-r][x])
				}

				if isValidTriangle(col...) {
					goodVertically++
				}
			}
		}

		if isValidTriangle(lineValues...) {
			goodHorizontal++
		}
	}

	fmt.Println("There is", goodHorizontal, "good", numAngles, "-angles horizontally")
	fmt.Println("There is", goodVertically, "good", numAngles, "-angles vertically")

	return 0
}

func isValidTriangle(nums ...int) bool {
	sort.Ints(nums)
	lastIndex := len(nums) - 1
	longestSide := nums[lastIndex]
	shortSidesSum := sum(nums[:lastIndex]...)

	return longestSide < shortSidesSum
}

func sum(nums ...int) int {
	total := 0
	for _, num := range nums {
		total += num
	}
	return total
}

func convertLine(line string) ([]int, error) {
	var result []int
	line = strings.TrimSpace(line)
	if line != "" {
		lineChuncks := strings.Split(line, " ")
		for _, value := range lineChuncks {
			if value != "" {
				number, err := strconv.Atoi(value)
				if err != nil {
					return nil, ErrInvalidInputFile
				}
				result = append(result, number)
			}
		}
	} else {
		return nil, ErrInvalidInputFile
	}
	return result, nil
}

func main() {
	fmt.Println("+-------------------------+")
	fmt.Println("| Advent of Code - Day 03 |")
	fmt.Println("+-------------------------+")

	result := run()
	os.Exit(result)
}
