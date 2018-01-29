package main

import (
	"fmt"
	"log"
	"math/rand"
	"os"
	"strconv"
	"sync"
	"time"
)

var (
	wg sync.WaitGroup
	n  int
)

func init() {
	fmt.Println("init...")
	log.SetOutput(os.Stdout)
	n = 1000
	fmt.Println("init done")
}

func main() {
	log.Print("main starting...")
	wg.Add(n)
	for i := 0; i < n; i++ {
		go print(strconv.Itoa(i))
	}
	wg.Wait()
	log.Print("main ended.")
}

func print(id string) {
	defer wg.Done()

	waitDuration := time.Duration(rand.Intn(5000)) * time.Millisecond
	log.Printf("%s is working", id)
	time.Sleep(waitDuration)
	log.Printf("%s is done and worked %d ms", id, waitDuration/time.Millisecond)
}
