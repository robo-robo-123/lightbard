﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lightbard.Models
{
  public class CommandModel : Common.BindableBase
  {
    public static CommandModel Instance { get; } = new CommandModel();

    public void RetweetToast()
    {


    }

  }
}
