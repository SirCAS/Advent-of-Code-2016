package main

import (
	"fmt"
	"io/ioutil"
	"math"
	"os"
	"strconv"
	"strings"
)

const (
	North int = iota
	East
	South
	West
)

type location struct {
	x int
	y int
}

func run() int {
	if len(os.Args) != 2 {
		fmt.Fprintf(os.Stderr, "%s filename\n", os.Args[0])
		return 1
	}

	b, err := ioutil.ReadFile(os.Args[1])
	if err != nil {
		fmt.Fprintln(os.Stderr, err)
		return 1
	}

	movements := make(map[int]int, 0)
	visited := make([]location, 0)

	currentDirection := North

	pos := location{0, 0}

	instructions := strings.Split(string(b), ", ")

	for _, instruction := range instructions {

		turn := int(instruction[0])
		distance, _ := strconv.Atoi(instruction[1:])

		if turn == 'R' {
			currentDirection++
		} else {
			currentDirection--
		}

		// TODO: Investigate if Python-like enums could be used such this if can be avoided
		if currentDirection < 0 {
			currentDirection = 3
		}
		currentDirection %= 4

		for i := 0; i < distance; i++ {

			movements[currentDirection]++

			pos = location{movements[East] - movements[West], movements[North] - movements[South]}

			if Any(visited, pos) {
				blocks := math.Abs(float64(pos.x)) + math.Abs(float64(pos.y))
				fmt.Println("Location is already visited @ (", pos.x, ",", pos.y, ") => ", blocks, " blocks away")
			} else {
				visited = append(visited, pos)
			}
		}
	}

	blocks := math.Abs(float64(pos.x)) + math.Abs(float64(pos.y))
	fmt.Println("Final location is @ (", pos.x, ",", pos.y, ") => ", blocks, " blocks away")

	return 0
}

func Any(all []location, target location) bool {
	for _, pos := range all {
		if pos.x == target.x && pos.y == target.y {
			return true
		}
	}
	return false
}

func main() {
	fmt.Println("+-------------------------+")
	fmt.Println("| Advent of Code - Day 01 |")
	fmt.Println("+-------------------------+")

	result := run()
	os.Exit(result)
}
