#include <iostream>
#include <cmath>
#include <vector>
#include <random>
#include <chrono>
#include <thread>

using namespace std;

#ifndef M_PI
#define M_PI 3.14159265358979323846
#endif

// Task1
class Bee {
public:
    pair<double, double> InitialPosition;
    pair<double, double> CurrentPosition;
    pair<double, double> TargetPosition;
    double Speed;
    bool goingToTarget;

    Bee(double initialX, double initialY, double speed)
        : InitialPosition(make_pair(initialX, initialY)),
        CurrentPosition(InitialPosition),
        TargetPosition(make_pair(0, 0)),
        Speed(speed),
        goingToTarget(true) {}

    void Move() {
        pair<double, double> target = goingToTarget ? TargetPosition : InitialPosition;

        double deltaX = target.first - CurrentPosition.first;
        double deltaY = target.second - CurrentPosition.second;
        double distance = sqrt(deltaX * deltaX + deltaY * deltaY);

        if (distance < Speed) {
            CurrentPosition = target;
            goingToTarget = !goingToTarget;
        }
        else {
            double ratio = Speed / distance;
            CurrentPosition.first += deltaX * ratio;
            CurrentPosition.second += deltaY * ratio;
        }
    }

    void PrintPosition() {
        cout << "Bee at (" << fixed << CurrentPosition.first << ", " << CurrentPosition.second << ") moving to "
            << (goingToTarget ? "target" : "initial position") << endl;
    }
};
// Task1

// Task2
class Drone {
private:
    static random_device rd;
    static mt19937 gen;
    uniform_real_distribution<double> angleDist;

public:
    pair<double, double> CurrentPosition;
    double Speed;
    double angle;
    chrono::time_point<chrono::steady_clock> lastDirectionChange;
    int directionChangeInterval;

    Drone(double initialX, double initialY, double speed, int directionChangeInterval)
        : CurrentPosition(make_pair(initialX, initialY)),
        Speed(speed),
        angleDist(0, 2 * M_PI),
        directionChangeInterval(directionChangeInterval) {
        ChangeDirection();
        lastDirectionChange = chrono::steady_clock::now();
    }

    void ChangeDirection() {
        angle = angleDist(gen);
    }

    void Move() {
        auto now = chrono::steady_clock::now();
        double elapsedSeconds = chrono::duration<double>(now - lastDirectionChange).count();

        if (elapsedSeconds >= directionChangeInterval) {
            ChangeDirection();
            lastDirectionChange = now;
        }

        CurrentPosition.first += Speed * cos(angle);
        CurrentPosition.second += Speed * sin(angle);
    }

    void PrintPosition() {
        cout << "Drone at (" << fixed << CurrentPosition.first << ", " << CurrentPosition.second << ") moving with speed " << Speed << endl;
    }
};

random_device Drone::rd;
mt19937 Drone::gen(Drone::rd());

int main() {
    cout << "Select task (1-2): ";
    int task;
    cin >> task;

    if (task == 1) {
        const int beeCount = 10;
        const double V = 1.0;
        vector<Bee> bees;

        for (int i = 0; i < beeCount; ++i) {
            double initialX = static_cast<double>(rand()) / RAND_MAX * 100;
            double initialY = static_cast<double>(rand()) / RAND_MAX * 100;
            bees.emplace_back(initialX, initialY, V);
        }

        while (true) {
            system("cls");
            for (auto& bee : bees) {
                bee.Move();
                bee.PrintPosition();
            }
            this_thread::sleep_for(chrono::milliseconds(100));
        }
    }
    else if (task == 2) {
        const int droneCount = 10;
        const double V = 1.0;
        const int directionChangeInterval = 2;
        vector<Drone> drones;

        for (int i = 0; i < droneCount; ++i) {
            double initialX = static_cast<double>(rand()) / RAND_MAX * 100;
            double initialY = static_cast<double>(rand()) / RAND_MAX * 100;
            drones.emplace_back(initialX, initialY, V, directionChangeInterval);
        }

        while (true) {
            system("cls");
            for (auto& drone : drones) {
                drone.Move();
                drone.PrintPosition();
            }
            this_thread::sleep_for(chrono::milliseconds(100));
        }
    }
    else {
        cout << "This task doesn't exist!" << endl;
    }

    return 0;
}
