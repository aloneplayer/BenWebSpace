
function PicScroller(scrollContId, leftButtonId, rightButtonId)
{
    this.scrollContId = scrollContId;
    this.leftButtonId = leftButtonId;
    this.rightButtonId = rightButtonId;
    this.dotClassName = "dotItem";
    this.dotOnClassName = "dotItemOn";
    this.dotObjArr = [];
    this.pageWidth = 300;
    this.frameWidth = 0;
    this.speed = 1;
    this.space = 10;
    this.pageIndex = 0;
    this.autoPlay = true;
    this.autoPlayTime = 5;
    var _autoTimeObj, _scrollTimeObj, _state = "ready";
    this.stripDiv = document.createElement("DIV");
    this.listDiv01 = document.createElement("DIV");
    this.listDiv02 = document.createElement("DIV");
    if (!PicScroller.childs)
    {
        PicScroller.childs = []
    }
    ;
    this.ID = PicScroller.childs.length;
    PicScroller.childs.push(this);
    this.initialize = function ()
    {
        if (!this.scrollContId)
        {
            throw new Error("必须指定scrollContId.");
            return;
        }

        this.scrollContDiv = $(this.scrollContId)[0];
        if (!this.scrollContDiv)
        {
            throw new Error("scrollContId不是正确的对象.(scrollContId = \"" + this.scrollContId + "\")");
            return;
        }

        this.scrollContDiv.style.width = this.scrollContDiv.offsetWidth + "px";
        this.scrollContDiv.style.overflow = "hidden";

        this.listDiv01.innerHTML = this.scrollContDiv.innerHTML + this.scrollContDiv.innerHTML;
        this.listDiv02.innerHTML = this.scrollContDiv.innerHTML + this.scrollContDiv.innerHTML;

        this.scrollContDiv.innerHTML = "";
        this.scrollContDiv.appendChild(this.stripDiv);
        this.stripDiv.appendChild(this.listDiv01);
        this.stripDiv.appendChild(this.listDiv02);
        this.stripDiv.style.overflow = "hidden";
        this.stripDiv.style.zoom = "1";
        this.stripDiv.style.width = "32766px";
        this.listDiv01.style.cssFloat = "left";
        this.listDiv02.style.cssFloat = "left";
        $(this.scrollContId).bind("mouseover", Function("PicScroller.childs[" + this.ID + "].stop()"));
        $(this.scrollContId).bind("mouseout", Function("PicScroller.childs[" + this.ID + "].play()"));

        if (this.leftButtonId)
        {
            $(this.leftButtonId).bind("mousedown", Function("PicScroller.childs[" + this.ID + "].rightMouseDown()"));
            $(this.leftButtonId).bind("mouseup", Function("PicScroller.childs[" + this.ID + "].rightEnd()"));
            $(this.leftButtonId).bind("mouseout", Function("PicScroller.childs[" + this.ID + "].rightEnd()"));
        }

        if (this.rightButtonId)
        {
            $(this.rightButtonId).bind("mousedown", Function("PicScroller.childs[" + this.ID + "].leftMouseDown()"));
            $(this.rightButtonId).bind("mouseup", Function("PicScroller.childs[" + this.ID + "].leftEnd()"));
            $(this.rightButtonId).bind("mouseout", Function("PicScroller.childs[" + this.ID + "].leftEnd()"));
        }

        if (this.autoPlay)
        {
            this.play()
        }
    };
    this.leftMouseDown = function ()
    {
        if (_state != "ready")
        {
            return
        }
        ;
        _state = "floating";
        _scrollTimeObj = setInterval("PicScroller.childs[" + this.ID + "].moveLeft()", this.speed)
    };
    this.rightMouseDown = function ()
    {
        if (_state != "ready")
        {
            return
        }
        ;
        _state = "floating";
        _scrollTimeObj = setInterval("PicScroller.childs[" + this.ID + "].moveRight()", this.speed)
    };
    this.moveLeft = function ()
    {
        this.stop();
        if (this.scrollContDiv.scrollLeft + this.space >= this.listDiv01.scrollWidth)
        {
            this.scrollContDiv.scrollLeft = this.scrollContDiv.scrollLeft + this.space - this.listDiv01.scrollWidth
        } else
        {
            this.scrollContDiv.scrollLeft += this.space
        }
        ;
        this.accountPageIndex()
    };
    this.moveRight = function ()
    {
        this.stop();
        if (this.scrollContDiv.scrollLeft - this.space <= 0)
        {
            this.scrollContDiv.scrollLeft = this.listDiv01.scrollWidth + this.scrollContDiv.scrollLeft - this.space
        } else
        {
            this.scrollContDiv.scrollLeft -= this.space
        }
        ;
        this.accountPageIndex()
    };
    this.leftEnd = function ()
    {
        if (_state != "floating")
        {
            return
        }
        ;
        _state = "stoping";
        clearInterval(_scrollTimeObj);
        var fill = this.pageWidth - this.scrollContDiv.scrollLeft % this.pageWidth;
        this.move(fill)
    };
    this.rightEnd = function ()
    {
        if (_state != "floating")
        {
            return
        }
        ;
        _state = "stoping";
        clearInterval(_scrollTimeObj);
        var fill = -this.scrollContDiv.scrollLeft % this.pageWidth;
        this.move(fill)
    };
    this.move = function (num, quick)
    {
        var thisMove = num / 5;
        if (!quick)
        {
            if (thisMove > this.space)
            {
                thisMove = this.space
            }
            ;
            if (thisMove < -this.space)
            {
                thisMove = -this.space
            }
        };
        if (Math.abs(thisMove) < 1 && thisMove != 0)
        {
            thisMove = thisMove >= 0 ? 1 : -1
        } else
        {
            thisMove = Math.round(thisMove)
        }
        ;
        var temp = this.scrollContDiv.scrollLeft + thisMove;
        if (thisMove > 0)
        {
            if (this.scrollContDiv.scrollLeft + thisMove >= this.listDiv01.scrollWidth)
            {
                this.scrollContDiv.scrollLeft = this.scrollContDiv.scrollLeft
                        + thisMove - this.listDiv01.scrollWidth
            } else
            {
                this.scrollContDiv.scrollLeft += thisMove
            }
        } else
        {
            if (this.scrollContDiv.scrollLeft - thisMove <= 0)
            {
                this.scrollContDiv.scrollLeft = this.listDiv01.scrollWidth
                        + this.scrollContDiv.scrollLeft - thisMove
            }
            else
            {
                this.scrollContDiv.scrollLeft += thisMove
            }
        }
        ;
        num -= thisMove;
        if (Math.abs(num) == 0)
        {
            _state = "ready";
            if (this.autoPlay)
            {
                this.play()
            }
            ;
            this.accountPageIndex();
            return
        } else
        {
            this.accountPageIndex();
            setTimeout("PicScroller.childs[" + this.ID + "].move(" + num + "," + quick + ")", this.speed)
        }
    };
    this.next = function ()
    {
        if (_state != "ready")
        {
            return
        };
        _state = "stoping";
        this.move(this.autoPlaySpace, true);
    };
    this.play = function ()
    {
        if (!this.autoPlay)
        {
            return
        };
        clearInterval(_autoTimeObj);
        _autoTimeObj = setInterval("PicScroller.childs[" + this.ID + "].next()", this.autoPlayTime);
    };
    this.stop = function ()
    {
        clearInterval(_autoTimeObj)
    };
    this.pageTo = function (num)
    {
        if (_state != "ready")
        {
            return
        }
        ;
        _state = "stoping";
        var fill = num * this.frameWidth - this.scrollContDiv.scrollLeft;
        this.move(fill, true)
    };
    this.accountPageIndex = function ()
    {
        this.pageIndex = Math.round(this.scrollContDiv.scrollLeft / this.frameWidth);
        if (this.pageIndex > Math.round(this.listDiv01.offsetWidth / this.frameWidth + 0.4) - 1)
        {
            this.pageIndex = 0
        }
        ;
        var i;
        for (i = 0; i < this.dotObjArr.length; i++)
        {
            if (i == this.pageIndex)
            {
                this.dotObjArr[i].className = this.dotClassName
            } else
            {
                this.dotObjArr[i].className = this.dotOnClassName
            }
        }
    }
};