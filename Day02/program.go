package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strconv"
)

const Up = 'U'
const Down = 'D'
const Left = 'L'
const Right = 'R'

func run() int {
	if len(os.Args) != 2 {
		fmt.Fprintf(os.Stderr, "%s filename\n", os.Args[0])
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

	threeByThreeKeypadCode := ThreeByThreeKeypadCode(lines)
	crazyKeypadCode := CrazyKeypadCode(lines)

	fmt.Println("3x3 code is:", threeByThreeKeypadCode)
	fmt.Println("Crazy code is:", crazyKeypadCode)

	return 0
}

func CrazyKeypadCode(lines []string) string {
	code := ""
	x := 0
	y := 2

	keyPad := [5][5]rune{[5]rune{0, 0, '1', 0, 0}, [5]rune{0, '2', '3', '4', 0}, [5]rune{'5', '6', '7', '8', '9'}, [5]rune{0, 'A', 'B', 'C', 0}, [5]rune{0, 0, 'D', 0, 0}}

	for _, line := range lines {
		for _, move := range line {
			switch move {
			case Up:
				if y > 0 && keyPad[y-1][x] != 0 {
					y--
				}
				break
			case Down:
				if y < 4 && keyPad[y+1][x] != 0 {
					y++
				}
				break
			case Right:
				if x < 4 && keyPad[y][x+1] != 0 {
					x++
				}
				break
			case Left:
				if x > 0 && keyPad[y][x-1] != 0 {
					x--
				}
				break
			}
		}
		code = code + string(keyPad[y][x])
	}

	return code
}

func ThreeByThreeKeypadCode(lines []string) string {
	code := ""
	pos := 5

	for _, line := range lines {
		for _, move := range line {
			switch move {
			case Up:
				if pos > 3 { // Only move if there an row above
					pos -= 3
				}
				break
			case Down:
				if pos < 7 { // Only move if there an row below
					pos += 3
				}
				break
			case Right:
				if pos%3 != 0 { // Only move if we're not at the end of a row
					pos++
				}
				break
			case Left:
				if pos%3 != 1 { // Only move if we're not at the beginning of a row
					pos--
				}
				break
			}
		}
		code = code + strconv.Itoa(pos)
	}

	return code
}

func main() {
	fmt.Println("+-------------------------+")
	fmt.Println("| Advent of Code - Day 02 |")
	fmt.Println("+-------------------------+")

	result := run()
	os.Exit(result)
}
