using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 德鲁伊 费用：4 攻击力：2 生命值：6
	//Forest Guardian
	//森林守护者
	//<b>Taunt</b>. Costs (1) less for each spell in your hand.
	//<b>嘲讽</b>。你手牌中每有一张法术牌，本牌的法力值消耗便减少（1）点。
	class Sim_EDR_107 : SimTemplate
	{
		// Cost reduction is handled by the card database's dynamic cost system.
		// This card inherently has Taunt which is defined in its card data.
		// For the sim, the cost reduction logic (spells in hand) is calculated
		// in the engine via getManaCost or similar hooks. No additional code needed here.

	}
}
