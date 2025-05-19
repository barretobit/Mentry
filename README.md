# 🧠 Mentry — Minimalist Daily Focus Tracker

**Mentry** is a minimalist WPF desktop app that helps you focus on your top 3 tasks each day.  
Built with modern C# .NET 9 and MVVM best practices, it features clean UI design, local JSON persistence, and a polished developer structure.

---

## ✨ Features

- ✅ Add up to 3 priority tasks per day
- 💾 Tasks are saved locally in JSON and persist across sessions
- 🕒 Auto-loads today’s tasks on startup
- 🌈 Beautiful and clean WPF styling
- 📦 MVVM architecture with Dependency Injection
- ⚡ Instant, responsive UI built with the [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet) library

---

## 📂 Project Structure

Mentry/
│
├── Models/ # TaskEntry and TaskItem data models
├── ViewModels/ # MainViewModel using CommunityToolkit.Mvvm
├── Views/ # MainWindow.xaml and related XAML UI
├── Services/ # IStorageService and JsonStorageService
├── Themes/ # Light color palette (Colors.xaml)
├── App.xaml # Theme merging and DI setup
└── Program.cs / App.xaml.cs # .NET 9 startup with DI

---

## 🛠️ Technologies Used

- [.NET 9 WPF](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/)
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)
- C# 13 / XAML
- MVVM architecture
- Local file-based storage (JSON)

---

## 📥 Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/barretobit/mentry.git

2. Open the solution in Visual Studio 2022

3. Build & run the project

--- 

## 🧪 Coming Soon

- 🌙 Dark Mode Toggle
- ⏱️ Task focus timers
- 📆 View past days
- 📤 Export productivity stats as JSON

---

## 📄 License
This project is licensed under the MIT License.

--- 

## 🙌 Author
- Mentry was created by me, João Barreto, as a demonstration of clean WPF + .NET 9 app architecture with a useful UI.
Follow me on GitHub for more C# projects.