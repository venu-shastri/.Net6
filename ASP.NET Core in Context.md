# ASP.NET Core in Context

![image-20220607211030754](E:\.net 6\Asp.net Core in context)



### Browser War

---

- JavaScript performance is paramount in modern browsers
- These Modern Browser engines use the latest optimization tricks like Just-In-Time (JIT) compilation where JavaScript
  gets converted into native code
- ![image-20220607212232021](E:\.net 6\JsToNativeCode)

- This process takes a lot of effort because JavaScript needs to be downloaded into the browser, where it gets parsed, then compiled into bytecode, and then Just-In-Time converted into native code

- Introducing **WebAssembly**

  >WebAssembly allows us to take the parsing and compiling to the server, before your users even open up their browser. With WebAssembly, you compile your code in a format called WASM (an abbreviation of WebASseMbly), which gets downloaded by the browser where it gets Just-In-Time compiled into native code

![image-20220607212458726](E:\.net 6\WebAssembly)

- Example : Open your browser and open https://earth.google.com. This should take you to
  the Google Earth app written in WebAssembly .  you will see that this application has excellent performance, but the
  initial load takes a fair amount of time because it needs to download the whole WASM application’s code.

- What is **WebAssembly**? 

  >**WebAssembly** (abbreviated Wasm) is a binary instruction format for a stack-based virtual machine. Wasm is designed as a portable target for compilation of high-level languages like C/C++/Rust, enabling deployment on the web for client and server applications

  >  **WebAssembly** is a new **binary format optimized** for **browser execution**, it is **NOT JavaScript**
  >
  > It uses a **stack-based virtual machine,**

- There are compilers for languages like C++ and Rust which compile to WASM. Some people have compiled C++ applications to WASM, allowing to run them in the browser

  - Windows 2000 operating system (https://bellard.org/jslinux/vm.html?url=https://bellard.org/jslinux/win2k.cfg&mem=192&graphic=1&w=1024&h=768)) compiled to WASM

### Which Browsers Support WebAssembly?

---

- WebAssembly is supported by all major browsers: Chrome, Edge, Safari, Opera, and Firefox, including their mobile versions
- https://caniuse.com/?search=WASM

### Mono

---

- Mono is an open source implementation of the .NET CLI specification (Mono is a platform for running .NET assemblies)
- Mono is used in Xamarin (now called Multi-platform App UI, or MAUI for short) for building mobile applications that run on
  the Windows, Android, and iOS mobile operating systems
- Mono run .NET on Linux (its original purpose) and is written in C++.



### Mono and WebAssembly

---

- Mono team decided to try to compile Mono to WebAssembly, which they did successfully
- Let take  Mono runtime and compiles it into WASM, and this runs in the browser where it will execute .NET Intermediate Language just like normal .NET does
  - This is the approach currently taken by Blazor. In the beginning, Blazor used the
    Mono runtime, but  now it uses  .NET Core runtime for WebAssembly

### Interacting with the Browser with Blazor

---

- WebAssembly with the .NET runtime allows  to run .NET code in the browser, Steve Sanderson used this to build **Blazor**
- Blazor uses the popular ASP.NET MVC approach for building applications that run in the browser
- Blazor project uses  razor files (Blazor = Browser + Razor) which execute inside to browser to dynamically build a web page
- With Blazor, team dont JavaScript to build a web app, which is good news for thousands of .NET developers
  who want to continue using C# (or F#) , To use some browser features, you will still need JavaScript



```html
@page "/counter"
<h1>Counter</h1>
<p role="status">Current count: @currentCount</p>
<button class="btn btn-primary" @onclick="IncrementCount"> Click me </button>

@code {
		private int currentCount = 0;
		private void IncrementCount()
		{
			currentCount++;
		}
	 }
```

![image-20220607215723237](E:\.net 6\rendertree)

### Blazor Server

---

- Blazor site is running on the server resulting in a way smaller download for the browser.
- The render tree is built on the server using regular .NET and then gets serialized to the browser using SignalR
- JavaScript in the browser then deserializes the render tree to update the DOM
- When User interact with the site, events get serialized back to the server which then executes the .NET code, updating the render tree, and the changes get serialized back to the browser.
- In this model  no need to send the .NET runtime and Blazor assemblies to the browser

![image-20220607220243964](E:\.net 6\BlazorServer)

#### Pros and Cons of the Blazor Server

---

- Smaller downloads
- Development process:
  - Blazor WebAssembly does not support all modern debugging capabilities,
- .NET APIs
- Online only
  - Running the Blazor application on the server does mean that users will always need access to the server. This will
    prevent the application from running in Electron, and as a Progressive Web Application (PWA).
- Server scalability:
  -  if server application need to support thousands of clients,  server(s) will have to handle all the work. Not
    only that, Blazor uses a stateful model which will require application to keep track of every user’s state on the server. So  server will need more resources than with Blazor WebAssembly which can use a stateless model.

