let commands = [];
    let commandIdx = 0;
    let lessonName = "";

    // data for input validation
    let allowedApps = ["git"];
    let allowedCommands = ["commit", "branch", "checkout"];
    
    //send request to check task result
    //change according to backend
    function checkTask() {
      let commandStrings = commands.map((commandData) => commandData.command);
      post('/Tasks/Check', {TaskName: lessonName, Commands : commandStrings}, "get");
    }

    // focus input on terminal click
    $('#yourdiv').click(function() {
      $('#yourfield').focus();
    });

    // saving enterd commands for task
    $(document).ready(() => {
      lessonName = $("#lesson-name").text();

      if(localStorage["lessonName"] == lessonName && localStorage["commands"].length > 0) {
        let savedCommands = JSON.parse(localStorage["commands"]);
        savedCommands.forEach(element => {
            insertCommand(element.command);
          });
      }
      else {
        localStorage["commands"] = "[]";
        localStorage["lessonName"] = lessonName;
      }
    });

    function deleteCommand(idx) {
      $("#command_" + idx + " i.icon-exclamation-sign").tooltip('hide');      
      $("#command_" + idx).remove();
      
      for(let i = 0; i < commands.length; i++) {
        if(commands[i].idx == idx) {
          commands.splice(i, 1);
          break;
        }
      }
      if(commands.length == 0)
        $("#check-btn").prop("disabled", true);
      localStorage["commands"] = JSON.stringify(commands);
    }
    function insertCommand(commandText) {
      let commandState = isCommandValid(commandText) ? "finished" : "error";
      let text = `<div id="command_${commandIdx}" class="reactCommandView" data-reactid=".1.$c47">
                <p class="${commandState} commandLine transitionBackground" data-reactid=".1.$c47.0">
                <span class="prompt" data-reactid=".1.$c47.0.0">$</span>
                <span data-reactid=".1.$c47.0.1"> </span>
                <span data-reactid=".1.$c47.0.2">${commandText}</span>
                <span class="icons transitionAllSlow" data-reactid=".1.$c47.0.3">
                  <i class="icon-exclamation-sign"` 
                  + (isCommandValid(commandText) ? '' : `onclick="deleteCommand(${commandIdx})" data-toggle="tooltip" data-placement="top" title="${validationMessage(commandText)}"`) +
                   `data-reactid=".1.$c47.0.3.0"></i>
                  <i class="icon-check-empty" data-reactid=".1.$c47.0.3.1"></i>
                  <i class="icon-retweet" data-reactid=".1.$c47.0.3.2"></i>
                  <i class="icon-check"`
                  + (isCommandValid(commandText) ? `onclick="deleteCommand(${commandIdx})"` : '') + 
                  `data-reactid=".1.$c47.0.3.3"></i>
                </span>
              </p>
              <div class="commandLineWarnings" data-reactid=".1.$c47.2"></div>
            </div>
            `;
      $("#commandDisplay").append(text);
      commands.push({idx : commandIdx++, command : commandText, valid : isCommandValid(commandText)});
      if(commands.length > 0)
        $("#check-btn").prop("disabled", false);
      localStorage["commands"] = JSON.stringify(commands);
      $('[data-toggle="tooltip"]').tooltip();   
    }
    
    function isCommandValid(commandText) {
      let words = commandText.match(/\S+/g) || [];
      return words.length > 1 && 
        allowedApps.includes(words[0]) && 
        allowedCommands.includes(words[1]);
    }
    
    function validationMessage(commandText) {
      let words = commandText.match(/\S+/g) || [];
      if(words.length < 2) {
        if(allowedApps.includes(words[0]))
          return "Потрібно ввести команду";
        return words[0] + " не є командою, або виконуваним файлом";        
      }
      if(!allowedApps.includes(words[0]))
        return words[0] + " не є командою, або виконуваним файлом"; 
      if(!allowedCommands.includes(words[1]))
        return "Не знайдена команда " + words[1]; 

      return "Помилка виконання команди";
    }

    // enter command
    $("#command-input").on('keyup', function (e) {
      if (e.keyCode == 13) {
        let t = $(this).val().toLowerCase();
        if(t !== "")
          insertCommand(t);
        $(this).val("");
      }
    });


    function post(path, params, method) {
      method = method || "post"; // Set method to post by default if not specified.
  
      var form = document.createElement("form");
      form.setAttribute("method", method);
      form.setAttribute("action", path);
  
      for(var key in params) {
          if(params.hasOwnProperty(key)) {
              var hiddenField = document.createElement("input");
              hiddenField.setAttribute("type", "hidden");
              hiddenField.setAttribute("name", key);
              hiddenField.setAttribute("value", params[key]);
  
              form.appendChild(hiddenField);
          }
      }
      document.body.appendChild(form);
      form.submit();
  }