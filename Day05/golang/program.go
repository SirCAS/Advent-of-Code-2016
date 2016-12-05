package main

import (
	"crypto/md5"
	"fmt"
	"os"
	"strconv"
)

func decryptWeak(seed string) string {
	i := 0
	result := ""

	for len(result) < 8 {
		input := append([]byte(seed), []byte(strconv.Itoa(i))...)
		hash := fmt.Sprintf("%x", md5.Sum(input))
		if hash[:5] == "00000" {
			result = result + string(hash[5])
		}

		i++
	}

	return result
}

func decryptBetter(seed string) string {

	i := 0
	hit := 0
	result := make([]byte, 8)

	for hit < 8 {
		input := append([]byte(seed), []byte(strconv.Itoa(i))...)
		hash := fmt.Sprintf("%x", md5.Sum(input))

		if hash[:5] == "00000" {
			index, err := strconv.Atoi(string(hash[5]))
			if err == nil && 0 <= index && index <= 7 && result[index] == 0 {
				result[index] = hash[6]
				hit++
			}
		}

		i++
	}

	return string(result)
}

func run() int {
	if len(os.Args) != 2 {
		fmt.Fprintf(os.Stderr, "%s [seed]\n", os.Args[0])
		return 1
	}

	part1 := decryptWeak(os.Args[1])
	fmt.Println("Part 1 password is", part1)

	part2 := decryptBetter(os.Args[1])
	fmt.Println("Part 2 password is", part2)

	return 0
}

func main() {
	fmt.Println("+-------------------------+")
	fmt.Println("| Advent of Code - Day 05 |")
	fmt.Println("+-------------------------+")

	result := run()
	os.Exit(result)
}
